namespace BudgetApp.API.Data.Repositories;

using BudgetApp.API.Models;

public interface ITransactionRepository : IRepository<Transaction>
{
    Task<IEnumerable<Transaction>> GetByUserIdAsync(int userId);
    Task<IEnumerable<Transaction>> GetByUserIdAndDateRangeAsync(int userId, DateTime startDate, DateTime endDate);
    Task<IEnumerable<Transaction>> GetByUserIdAndCategoryIdAsync(int userId, int categoryId);
} 