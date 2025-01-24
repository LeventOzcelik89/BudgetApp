namespace BudgetApp.API.DTOs.Reports;

public class MonthlyReportDto
{
    public int Year { get; set; }
    public int Month { get; set; }
    public decimal TotalIncome { get; set; }
    public decimal TotalExpense { get; set; }
    public decimal Balance { get; set; }
    public List<CategoryReportDto> Categories { get; set; }
    public List<BudgetComparisonDto> BudgetComparisons { get; set; }
}

public class CategoryReportDto
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public decimal Amount { get; set; }
    public decimal Percentage { get; set; }
    public decimal Change { get; set; } // Önceki aya göre değişim yüzdesi
}

public class BudgetComparisonDto
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public decimal PlannedAmount { get; set; }
    public decimal ActualAmount { get; set; }
    public decimal Difference { get; set; }
    public decimal Progress { get; set; } // Yüzde olarak ilerleme
}

public class YearlyReportDto
{
    public int Year { get; set; }
    public decimal TotalIncome { get; set; }
    public decimal TotalExpense { get; set; }
    public decimal Balance { get; set; }
    public List<MonthlyTrendDto> MonthlyTrends { get; set; }
    public List<CategoryReportDto> TopCategories { get; set; }
}

public class MonthlyTrendDto
{
    public int Month { get; set; }
    public decimal Income { get; set; }
    public decimal Expense { get; set; }
    public decimal Balance { get; set; }
} 