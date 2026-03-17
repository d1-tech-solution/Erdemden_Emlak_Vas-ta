using BussinessLayer.Abstract;
using Core.DTOs.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Erdemden.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "AdminOnly")]
public class AnalyticsController : ControllerBase
{
    private readonly IAnalyticsService _analyticsService;

    public AnalyticsController(IAnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }

    [HttpGet("overview")]
    public async Task<IActionResult> GetOverview([FromQuery] int days = 30)
    {
        if (days is not (7 or 30 or 90))
        {
            return BadRequest(ApiResponseDto.FailResponse("Geçersiz tarih aralığı. 7, 30 veya 90 gün seçilebilir."));
        }

        var result = await _analyticsService.GetOverviewAsync(days);

        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }
}
