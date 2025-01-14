namespace BudgetApp.API.Data.Repositories;

using BudgetApp.API.Models;
using BudgetApp.API.DTOs.Goals;

public interface IGoalRepository : IRepository<Goal>
{
    Task<IEnumerable<Goal>> GetByUserIdAsync(int userId);
    Task<IEnumerable<Goal>> GetActiveGoalsAsync(int userId);
    Task<Goal> GetByUserIdAndCategoryAsync(int userId, int categoryId);
    Task UpdateGoalProgressAsync(int goalId, decimal amount);
} 