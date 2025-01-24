namespace BudgetApp.API.Data.Repositories;

using BudgetApp.API.Models;

public interface ICategoryRepository : IRepository<Category>
{
    Task<IEnumerable<Category>> GetByUserIdAsync(int userId);
} 