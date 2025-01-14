namespace BudgetApp.API.Data.Repositories;

using Microsoft.EntityFrameworkCore;
using BudgetApp.API.Models;
using BudgetApp.API.DTOs.Goals;

public class GoalRepository : Repository<Goal>, IGoalRepository
{
    public GoalRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Goal>> GetByUserIdAsync(int userId)
    {
        return await _dbSet
            .Include(g => g.Category)
            .Where(g => g.UserId == userId && !g.IsDeleted)
            .OrderByDescending(g => g.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Goal>> GetActiveGoalsAsync(int userId)
    {
        return await _dbSet
            .Include(g => g.Category)
            .Where(g => g.UserId == userId && 
                       !g.IsDeleted && 
                       g.Status == GoalStatus.Active)
            .ToListAsync();
    }

    public async Task<Goal> GetByUserIdAndCategoryAsync(int userId, int categoryId)
    {
        return await _dbSet
            .Include(g => g.Category)
            .FirstOrDefaultAsync(g => g.UserId == userId && 
                                    g.CategoryId == categoryId && 
                                    !g.IsDeleted &&
                                    g.Status == GoalStatus.Active);
    }

    public async Task UpdateGoalProgressAsync(int goalId, decimal amount)
    {
        var goal = await _dbSet.FindAsync(goalId);
        if (goal != null)
        {
            goal.CurrentAmount += amount;
            if (goal.CurrentAmount >= goal.TargetAmount)
            {
                goal.Status = GoalStatus.Completed;
            }
            await _context.SaveChangesAsync();
        }
    }
} 