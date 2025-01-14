namespace BudgetApp.API.Data.Repositories;

using Microsoft.EntityFrameworkCore;
using BudgetApp.API.Models;

public class TransactionRepository : Repository<Transaction>, ITransactionRepository
{
    public TransactionRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Transaction>> GetByUserIdAsync(int userId)
    {
        return await _dbSet
            .Include(t => t.Category)
            .Where(t => t.UserId == userId && !t.IsDeleted)
            .OrderByDescending(t => t.TransactionDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Transaction>> GetByUserIdAndDateRangeAsync(int userId, DateTime startDate, DateTime endDate)
    {
        return await _dbSet
            .Include(t => t.Category)
            .Where(t => t.UserId == userId && 
                       !t.IsDeleted && 
                       t.TransactionDate >= startDate && 
                       t.TransactionDate <= endDate)
            .OrderByDescending(t => t.TransactionDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Transaction>> GetByUserIdAndCategoryAsync(int userId, int categoryId)
    {
        return await _dbSet
            .Include(t => t.Category)
            .Where(t => t.UserId == userId && 
                       t.CategoryId == categoryId && 
                       !t.IsDeleted)
            .OrderByDescending(t => t.TransactionDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Transaction>> GetByUserIdAndCategoryIdAsync(int userId, int categoryId)
    {
        return await _dbSet
            .Where(t => t.UserId == userId && t.CategoryId == categoryId && !t.IsDeleted)
            .Include(t => t.Category)
            .Include(t => t.Currency)
            .ToListAsync();
    }
} 