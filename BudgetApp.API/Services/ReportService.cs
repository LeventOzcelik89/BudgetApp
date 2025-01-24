namespace BudgetApp.API.Services;

using BudgetApp.API.Data.Repositories;
using BudgetApp.API.DTOs.Reports;
using BudgetApp.API.Models.Enums;

public class ReportService : IReportService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IBudgetRepository _budgetRepository;

    public ReportService(
        ITransactionRepository transactionRepository,
        IBudgetRepository budgetRepository)
    {
        _transactionRepository = transactionRepository;
        _budgetRepository = budgetRepository;
    }

    public async Task<MonthlyReportDto> GetMonthlyReportAsync(int userId, int year, int month)
    {
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);
        var previousMonth = startDate.AddMonths(-1);

        var currentTransactions = await _transactionRepository.GetByUserIdAndDateRangeAsync(userId, startDate, endDate);
        var previousTransactions = await _transactionRepository.GetByUserIdAndDateRangeAsync(userId, previousMonth, startDate.AddDays(-1));
        var budgets = await _budgetRepository.GetActiveBudgetsAsync(userId, startDate);

        var report = new MonthlyReportDto
        {
            Year = year,
            Month = month,
            TotalIncome = currentTransactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.ConvertedAmount),
            TotalExpense = currentTransactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.ConvertedAmount),
            Categories = new List<CategoryReportDto>(),
            BudgetComparisons = new List<BudgetComparisonDto>()
        };

        report.Balance = report.TotalIncome - report.TotalExpense;

        // Kategori bazlı raporlama
        var categoryGroups = currentTransactions.GroupBy(t => t.Category);
        foreach (var group in categoryGroups)
        {
            var currentAmount = group.Sum(t => t.ConvertedAmount);
            var previousAmount = previousTransactions
                .Where(t => t.CategoryId == group.Key.Id)
                .Sum(t => t.ConvertedAmount);

            var change = previousAmount > 0 
                ? ((currentAmount - previousAmount) / previousAmount) * 100 
                : 100;

            report.Categories.Add(new CategoryReportDto
            {
                CategoryId = group.Key.Id,
                CategoryName = group.Key.Name,
                Amount = currentAmount,
                Percentage = currentAmount / (group.First().Type == TransactionType.Income ? report.TotalIncome : report.TotalExpense) * 100,
                Change = change
            });
        }

        // Bütçe karşılaştırması
        foreach (var budget in budgets)
        {
            var actualAmount = currentTransactions
                .Where(t => t.CategoryId == budget.CategoryId)
                .Sum(t => t.ConvertedAmount);

            report.BudgetComparisons.Add(new BudgetComparisonDto
            {
                CategoryId = budget.CategoryId,
                CategoryName = budget.Category.Name,
                PlannedAmount = budget.PlannedAmount,
                ActualAmount = actualAmount,
                Difference = budget.PlannedAmount - actualAmount,
                Progress = (actualAmount / budget.PlannedAmount) * 100
            });
        }

        return report;
    }

    public async Task<YearlyReportDto> GetYearlyReportAsync(int userId, int year)
    {
        var startDate = new DateTime(year, 1, 1);
        var endDate = startDate.AddYears(1).AddDays(-1);

        var transactions = await _transactionRepository.GetByUserIdAndDateRangeAsync(userId, startDate, endDate);

        var report = new YearlyReportDto
        {
            Year = year,
            TotalIncome = transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.ConvertedAmount),
            TotalExpense = transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.ConvertedAmount),
            MonthlyTrends = new List<MonthlyTrendDto>(),
            TopCategories = new List<CategoryReportDto>()
        };

        report.Balance = report.TotalIncome - report.TotalExpense;

        // Aylık trendler
        for (int month = 1; month <= 12; month++)
        {
            var monthStart = new DateTime(year, month, 1);
            var monthEnd = monthStart.AddMonths(1).AddDays(-1);

            var monthlyTransactions = transactions.Where(t => 
                t.TransactionDate >= monthStart && 
                t.TransactionDate <= monthEnd);

            report.MonthlyTrends.Add(new MonthlyTrendDto
            {
                Month = month,
                Income = monthlyTransactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.ConvertedAmount),
                Expense = monthlyTransactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.ConvertedAmount),
                Balance = monthlyTransactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.ConvertedAmount) -
                         monthlyTransactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.ConvertedAmount)
            });
        }

        // En çok harcama yapılan kategoriler
        var categoryGroups = transactions
            .GroupBy(t => t.Category)
            .OrderByDescending(g => g.Sum(t => t.ConvertedAmount))
            .Take(5);

        foreach (var group in categoryGroups)
        {
            var amount = group.Sum(t => t.ConvertedAmount);
            report.TopCategories.Add(new CategoryReportDto
            {
                CategoryId = group.Key.Id,
                CategoryName = group.Key.Name,
                Amount = amount,
                Percentage = amount / (group.First().Type == TransactionType.Income ? report.TotalIncome : report.TotalExpense) * 100
            });
        }

        return report;
    }

    public async Task<IEnumerable<MonthlyTrendDto>> GetTrendsAsync(int userId, DateTime startDate, DateTime endDate)
    {
        var transactions = await _transactionRepository.GetByUserIdAndDateRangeAsync(userId, startDate, endDate);
        var trends = new List<MonthlyTrendDto>();

        for (var date = startDate; date <= endDate; date = date.AddMonths(1))
        {
            var monthEnd = date.AddMonths(1).AddDays(-1);
            var monthlyTransactions = transactions.Where(t => 
                t.TransactionDate >= date && 
                t.TransactionDate <= monthEnd);

            trends.Add(new MonthlyTrendDto
            {
                Month = date.Month,
                Income = monthlyTransactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.ConvertedAmount),
                Expense = monthlyTransactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.ConvertedAmount),
                Balance = monthlyTransactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.ConvertedAmount) -
                         monthlyTransactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.ConvertedAmount)
            });
        }

        return trends;
    }
} 