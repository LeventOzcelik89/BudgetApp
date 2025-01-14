namespace BudgetApp.API.Services;

using BudgetApp.API.DTOs.Goals;

public interface IGoalService
{
    Task<IEnumerable<GoalDto>> GetAllAsync(int userId);
    Task<GoalDto> GetByIdAsync(int userId, int goalId);
    Task<IEnumerable<GoalDto>> GetActiveGoalsAsync(int userId);
    Task<GoalDto> CreateAsync(int userId, CreateGoalDto dto);
    Task<GoalDto> UpdateAsync(int userId, int goalId, UpdateGoalDto dto);
    Task DeleteAsync(int userId, int goalId);
    Task UpdateProgressAsync(int userId, int goalId, decimal amount);
} 