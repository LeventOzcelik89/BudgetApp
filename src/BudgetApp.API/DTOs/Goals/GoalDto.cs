namespace BudgetApp.API.DTOs.Goals;

public class GoalDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal TargetAmount { get; set; }
    public decimal CurrentAmount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime TargetDate { get; set; }
    public GoalStatus Status { get; set; }
    public decimal Progress => (CurrentAmount / TargetAmount) * 100;
    public int? CategoryId { get; set; }
    public string CategoryName { get; set; }
}

public class CreateGoalDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal TargetAmount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime TargetDate { get; set; }
    public int? CategoryId { get; set; }
}

public class UpdateGoalDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal TargetAmount { get; set; }
    public DateTime TargetDate { get; set; }
    public GoalStatus Status { get; set; }
}

public enum GoalStatus
{
    Active = 1,
    Completed = 2,
    Cancelled = 3
} 