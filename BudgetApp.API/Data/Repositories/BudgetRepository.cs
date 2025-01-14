namespace BudgetApp.API.Data.Repositories;

using Microsoft.EntityFrameworkCore;
using BudgetApp.API.Models;

public class BudgetRepository : Repository<Budget>, IBudgetRepository
{
    public BudgetRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Budget>> GetByUserIdAsync(int userId)
    {
        return await _dbSet
            .Include(b => b.Category)
            .Where(b => b.UserId == userId && !b.IsDeleted)
            .OrderByDescending(b => b.StartDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Budget>> GetActiveBudgetsAsync(int userId, DateTime date)
    {
        return await _dbSet
            .Include(b => b.Category)
            .Where(b => b.UserId == userId && 
                       !b.IsDeleted && 
                       b.StartDate <= date && 
                       b.EndDate >= date)
            .ToListAsync();
    }

    public async Task<Budget> GetByUserIdAndCategoryAsync(int userId, int categoryId, DateTime date)
    {
        return await _dbSet
            .Include(b => b.Category)
            .FirstOrDefaultAsync(b => b.UserId == userId && 
                                    b.CategoryId == categoryId && 
                                    !b.IsDeleted && 
                                    b.StartDate <= date && 
                                    b.EndDate >= date);
    }
} 