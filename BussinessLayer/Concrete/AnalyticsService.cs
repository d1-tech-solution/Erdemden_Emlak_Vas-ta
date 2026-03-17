using BussinessLayer.Abstract;
using BussinessLayer.Settings;
using Core.DTOs.AnalyticsDtos;
using Core.DTOs.Common;
using Google.Apis.AnalyticsData.v1beta;
using Google.Apis.AnalyticsData.v1beta.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace BussinessLayer.Concrete;

public class AnalyticsService : IAnalyticsService
{
    private readonly GoogleAnalyticsSettings _settings;
    private readonly IMemoryCache _cache;

    public AnalyticsService(IOptions<GoogleAnalyticsSettings> settings, IMemoryCache cache)
    {
        _settings = settings.Value;
        _cache = cache;
    }

    public async Task<ApiResponseDto<AnalyticsOverviewDto>> GetOverviewAsync(int days)
    {
        var cacheKey = $"analytics_overview_{days}";
        if (_cache.TryGetValue(cacheKey, out AnalyticsOverviewDto? cached) && cached != null)
        {
            return ApiResponseDto<AnalyticsOverviewDto>.SuccessResponse(cached);
        }

        try
        {
            var credential = GoogleCredential
                .FromFile(_settings.CredentialsFilePath)
                .CreateScoped("https://www.googleapis.com/auth/analytics.readonly");

            var service = new BetaAnalyticsDataService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "ErdemdenAnalytics"
            });

            var propertyId = $"properties/{_settings.PropertyId}";
            var startDate = $"{days}daysAgo";
            var endDate = "today";

            // 4 paralel rapor çağrısı
            var dailyTask = RunDailyMetricsReport(service, propertyId, startDate, endDate);
            var trafficTask = RunTrafficSourcesReport(service, propertyId, startDate, endDate);
            var pagesTask = RunPopularPagesReport(service, propertyId, startDate, endDate);
            var devicesTask = RunDeviceBreakdownReport(service, propertyId, startDate, endDate);

            await Task.WhenAll(dailyTask, trafficTask, pagesTask, devicesTask);

            var dailyMetrics = dailyTask.Result;
            var trafficSources = trafficTask.Result;
            var popularPages = pagesTask.Result;
            var deviceBreakdown = devicesTask.Result;

            var overview = new AnalyticsOverviewDto
            {
                TotalVisitors = dailyMetrics.Sum(d => d.Visitors),
                TotalPageViews = dailyMetrics.Sum(d => d.PageViews),
                TotalSessions = dailyMetrics.Sum(d => d.Sessions),
                BounceRate = dailyMetrics.Count > 0
                    ? Math.Round(dailyMetrics.Average(d => d.BounceRate), 2)
                    : 0,
                DailyMetrics = dailyMetrics
                    .Select(d => new DailyMetricDto
                    {
                        Date = d.Date,
                        Visitors = d.Visitors,
                        PageViews = d.PageViews,
                        Sessions = d.Sessions
                    })
                    .OrderBy(d => d.Date)
                    .ToList(),
                TrafficSources = trafficSources,
                PopularPages = popularPages,
                DeviceBreakdown = deviceBreakdown
            };

            _cache.Set(cacheKey, overview, TimeSpan.FromMinutes(5));

            return ApiResponseDto<AnalyticsOverviewDto>.SuccessResponse(overview);
        }
        catch (Exception ex)
        {
            return ApiResponseDto<AnalyticsOverviewDto>.FailResponse(
                $"Google Analytics verileri alınırken hata oluştu: {ex.Message}");
        }
    }

    private async Task<List<DailyMetricRaw>> RunDailyMetricsReport(
        BetaAnalyticsDataService service, string propertyId, string startDate, string endDate)
    {
        var request = new RunReportRequest
        {
            DateRanges = new[] { new DateRange { StartDate = startDate, EndDate = endDate } },
            Dimensions = new[] { new Dimension { Name = "date" } },
            Metrics = new[]
            {
                new Metric { Name = "activeUsers" },
                new Metric { Name = "screenPageViews" },
                new Metric { Name = "sessions" },
                new Metric { Name = "bounceRate" }
            }
        };

        var response = await service.Properties.RunReport(request, propertyId).ExecuteAsync();

        var results = new List<DailyMetricRaw>();
        if (response.Rows == null) return results;

        foreach (var row in response.Rows)
        {
            var dateStr = row.DimensionValues[0].Value; // "20260317" format
            var formatted = $"{dateStr[..4]}-{dateStr[4..6]}-{dateStr[6..8]}";

            results.Add(new DailyMetricRaw
            {
                Date = formatted,
                Visitors = int.TryParse(row.MetricValues[0].Value, out var v) ? v : 0,
                PageViews = int.TryParse(row.MetricValues[1].Value, out var pv) ? pv : 0,
                Sessions = int.TryParse(row.MetricValues[2].Value, out var s) ? s : 0,
                BounceRate = double.TryParse(row.MetricValues[3].Value, out var br) ? br : 0
            });
        }

        return results;
    }

    private async Task<List<TrafficSourceDto>> RunTrafficSourcesReport(
        BetaAnalyticsDataService service, string propertyId, string startDate, string endDate)
    {
        var request = new RunReportRequest
        {
            DateRanges = new[] { new DateRange { StartDate = startDate, EndDate = endDate } },
            Dimensions = new[] { new Dimension { Name = "sessionSource" } },
            Metrics = new[] { new Metric { Name = "sessions" } },
            OrderBys = new[] { new OrderBy { Metric = new OrderBy.MetricOrderBy { MetricName = "sessions" }, Desc = true } },
            Limit = 10
        };

        var response = await service.Properties.RunReport(request, propertyId).ExecuteAsync();

        var results = new List<TrafficSourceDto>();
        if (response.Rows == null) return results;

        var total = response.Rows.Sum(r => int.TryParse(r.MetricValues[0].Value, out var v) ? v : 0);

        foreach (var row in response.Rows)
        {
            var count = int.TryParse(row.MetricValues[0].Value, out var c) ? c : 0;
            results.Add(new TrafficSourceDto
            {
                Source = row.DimensionValues[0].Value ?? "(direct)",
                Count = count,
                Percentage = total > 0 ? Math.Round((double)count / total * 100, 1) : 0
            });
        }

        return results;
    }

    private async Task<List<PopularPageDto>> RunPopularPagesReport(
        BetaAnalyticsDataService service, string propertyId, string startDate, string endDate)
    {
        var request = new RunReportRequest
        {
            DateRanges = new[] { new DateRange { StartDate = startDate, EndDate = endDate } },
            Dimensions = new[]
            {
                new Dimension { Name = "pagePathPlusQueryString" },
                new Dimension { Name = "pageTitle" }
            },
            Metrics = new[] { new Metric { Name = "screenPageViews" } },
            OrderBys = new[] { new OrderBy { Metric = new OrderBy.MetricOrderBy { MetricName = "screenPageViews" }, Desc = true } },
            Limit = 10
        };

        var response = await service.Properties.RunReport(request, propertyId).ExecuteAsync();

        var results = new List<PopularPageDto>();
        if (response.Rows == null) return results;

        foreach (var row in response.Rows)
        {
            results.Add(new PopularPageDto
            {
                PagePath = row.DimensionValues[0].Value,
                PageTitle = row.DimensionValues[1].Value,
                Views = int.TryParse(row.MetricValues[0].Value, out var v) ? v : 0
            });
        }

        return results;
    }

    private async Task<List<DeviceBreakdownDto>> RunDeviceBreakdownReport(
        BetaAnalyticsDataService service, string propertyId, string startDate, string endDate)
    {
        var request = new RunReportRequest
        {
            DateRanges = new[] { new DateRange { StartDate = startDate, EndDate = endDate } },
            Dimensions = new[] { new Dimension { Name = "deviceCategory" } },
            Metrics = new[] { new Metric { Name = "activeUsers" } },
            OrderBys = new[] { new OrderBy { Metric = new OrderBy.MetricOrderBy { MetricName = "activeUsers" }, Desc = true } }
        };

        var response = await service.Properties.RunReport(request, propertyId).ExecuteAsync();

        var results = new List<DeviceBreakdownDto>();
        if (response.Rows == null) return results;

        var total = response.Rows.Sum(r => int.TryParse(r.MetricValues[0].Value, out var v) ? v : 0);

        foreach (var row in response.Rows)
        {
            var count = int.TryParse(row.MetricValues[0].Value, out var c) ? c : 0;
            results.Add(new DeviceBreakdownDto
            {
                DeviceCategory = row.DimensionValues[0].Value,
                Count = count,
                Percentage = total > 0 ? Math.Round((double)count / total * 100, 1) : 0
            });
        }

        return results;
    }

    private class DailyMetricRaw
    {
        public string Date { get; set; } = string.Empty;
        public int Visitors { get; set; }
        public int PageViews { get; set; }
        public int Sessions { get; set; }
        public double BounceRate { get; set; }
    }
}
