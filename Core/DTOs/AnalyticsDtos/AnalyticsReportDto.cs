namespace Core.DTOs.AnalyticsDtos;

public class AnalyticsOverviewDto
{
    public int TotalVisitors { get; set; }
    public int TotalPageViews { get; set; }
    public int TotalSessions { get; set; }
    public double BounceRate { get; set; }
    public List<DailyMetricDto> DailyMetrics { get; set; } = new();
    public List<TrafficSourceDto> TrafficSources { get; set; } = new();
    public List<PopularPageDto> PopularPages { get; set; } = new();
    public List<DeviceBreakdownDto> DeviceBreakdown { get; set; } = new();
}

public class DailyMetricDto
{
    public string Date { get; set; } = string.Empty;
    public int Visitors { get; set; }
    public int PageViews { get; set; }
    public int Sessions { get; set; }
}

public class TrafficSourceDto
{
    public string Source { get; set; } = string.Empty;
    public int Count { get; set; }
    public double Percentage { get; set; }
}

public class PopularPageDto
{
    public string PagePath { get; set; } = string.Empty;
    public string PageTitle { get; set; } = string.Empty;
    public int Views { get; set; }
}

public class DeviceBreakdownDto
{
    public string DeviceCategory { get; set; } = string.Empty;
    public int Count { get; set; }
    public double Percentage { get; set; }
}
