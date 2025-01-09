namespace BudgetApp.API.DTOs.Budget;

public class BudgetDto
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public decimal PlannedAmount { get; set; }
    public decimal ActualAmount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Progress => ActualAmount / PlannedAmount * 100;
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
    public decimal TotalPlannedAmount { get; set; }
    public decimal TotalActualAmount { get; set; }
    public decimal TotalProgress => TotalActualAmount / TotalPlannedAmount * 100;
    public List<BudgetDto> Budgets { get; set; }
} 