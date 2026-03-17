using Core.DTOs.AnalyticsDtos;
using Core.DTOs.Common;

namespace BussinessLayer.Abstract;

public interface IAnalyticsService
{
    Task<ApiResponseDto<AnalyticsOverviewDto>> GetOverviewAsync(int days);
}
