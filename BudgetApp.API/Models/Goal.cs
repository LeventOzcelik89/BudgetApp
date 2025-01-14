namespace BudgetApp.API.Models;

using BudgetApp.API.Models.Common;
using BudgetApp.API.DTOs.Goals;

public class Goal : BaseEntity
{
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal TargetAmount { get; set; }
    public decimal CurrentAmount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime TargetDate { get; set; }
    public GoalStatus Status { get; set; }
    public int? CategoryId { get; set; }

    public User User { get; set; }
    public Category Category { get; set; }
} 