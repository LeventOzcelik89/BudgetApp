namespace BudgetApp.API.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BudgetApp.API.Services;
using BudgetApp.API.DTOs.Reports;
using System.Security.Claims;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    private int GetUserId()
    {
        return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }

    [HttpGet("monthly/{year}/{month}")]
    public async Task<ActionResult<MonthlyReportDto>> GetMonthlyReport(int year, int month)
    {
        var userId = GetUserId();
        var report = await _reportService.GetMonthlyReportAsync(userId, year, month);
        return Ok(report);
    }

    [HttpGet("yearly/{year}")]
    public async Task<ActionResult<YearlyReportDto>> GetYearlyReport(int year)
    {
        var userId = GetUserId();
        var report = await _reportService.GetYearlyReportAsync(userId, year);
        return Ok(report);
    }

    [HttpGet("trends")]
    public async Task<ActionResult<IEnumerable<MonthlyTrendDto>>> GetTrends(
        [FromQuery] DateTime startDate, 
        [FromQuery] DateTime endDate)
    {
        var userId = GetUserId();
        var trends = await _reportService.GetTrendsAsync(userId, startDate, endDate);
        return Ok(trends);
    }
} 