namespace BudgetApp.API.Data.Repositories;

using BudgetApp.API.Models;

public interface IBudgetRepository : IRepository<Budget>
{
    Task<IEnumerable<Budget>> GetByUserIdAsync(int userId);
    Task<IEnumerable<Budget>> GetActiveBudgetsAsync(int userId, DateTime date);
    Task<Budget> GetByUserIdAndCategoryAsync(int userId, int categoryId, DateTime date);
} 