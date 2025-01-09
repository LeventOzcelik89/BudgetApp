namespace BudgetApp.API.DTOs.Budget;

public class BudgetDto
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public decimal PlannedAmount { get; set; }
    public decimal SpentAmount { get; set; }
    public decimal RemainingAmount { get; set; }
    public decimal SpentPercentage { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public class CreateBudgetDto
{
    public int CategoryId { get; set; }
    public decimal PlannedAmount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public class UpdateBudgetDto
{
    public decimal PlannedAmount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public class BudgetSummaryDto
{
    public decimal TotalPlannedAmount => Budgets?.Sum(b => b.PlannedAmount) ?? 0;
    public decimal TotalSpentAmount => Budgets?.Sum(b => b.SpentAmount) ?? 0;
    public decimal TotalRemainingAmount => TotalPlannedAmount - TotalSpentAmount;
    public decimal TotalSpentPercentage => TotalPlannedAmount > 0 ? (TotalSpentAmount / TotalPlannedAmount) * 100 : 0;
    public List<BudgetDto> Budgets { get; set; } = new();
} 