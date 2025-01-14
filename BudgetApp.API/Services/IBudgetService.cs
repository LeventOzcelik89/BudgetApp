namespace BudgetApp.API.Services;

using BudgetApp.API.DTOs.Budget;

public interface IBudgetService
{
    Task<IEnumerable<BudgetDto>> GetAllAsync(int userId);
    Task<BudgetDto> GetByIdAsync(int userId, int budgetId);
    Task<BudgetSummaryDto> GetSummaryAsync(int userId, DateTime date);
    Task<BudgetDto> CreateAsync(int userId, CreateBudgetDto dto);
    Task<BudgetDto> UpdateAsync(int userId, int budgetId, UpdateBudgetDto dto);
    Task DeleteAsync(int userId, int budgetId);
} 