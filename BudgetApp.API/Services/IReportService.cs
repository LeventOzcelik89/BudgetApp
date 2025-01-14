namespace BudgetApp.API.Services;

using BudgetApp.API.DTOs.Reports;

public interface IReportService
{
    Task<MonthlyReportDto> GetMonthlyReportAsync(int userId, int year, int month);
    Task<YearlyReportDto> GetYearlyReportAsync(int userId, int year);
    Task<IEnumerable<MonthlyTrendDto>> GetTrendsAsync(int userId, DateTime startDate, DateTime endDate);
} 