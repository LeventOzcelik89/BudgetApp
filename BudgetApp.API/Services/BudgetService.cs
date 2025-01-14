namespace BudgetApp.API.Services;

using AutoMapper;
using BudgetApp.API.Data.Repositories;
using BudgetApp.API.DTOs.Budget;
using BudgetApp.API.Models;
using BudgetApp.API.Models.Enums;

public class BudgetService : IBudgetService
{
    private readonly IBudgetRepository _budgetRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public BudgetService(
        IBudgetRepository budgetRepository,
        ITransactionRepository transactionRepository,
        ICategoryRepository categoryRepository,
        IMapper mapper)
    {
        _budgetRepository = budgetRepository;
        _transactionRepository = transactionRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BudgetDto>> GetAllAsync(int userId)
    {
        var budgets = await _budgetRepository.GetByUserIdAsync(userId);
        var budgetDtos = _mapper.Map<IEnumerable<BudgetDto>>(budgets);

        foreach (var budgetDto in budgetDtos)
        {
            var transactions = await _transactionRepository.GetByUserIdAndCategoryIdAsync(userId, budgetDto.CategoryId);
            var totalSpent = transactions
                .Where(t => t.Type == TransactionType.Expense && 
                       t.TransactionDate >= budgetDto.StartDate && 
                       t.TransactionDate <= budgetDto.EndDate)
                .Sum(t => t.ConvertedAmount);

            budgetDto.SpentAmount = totalSpent;
            budgetDto.RemainingAmount = budgetDto.PlannedAmount - totalSpent;
            budgetDto.SpentPercentage = budgetDto.PlannedAmount > 0 ? (totalSpent / budgetDto.PlannedAmount) * 100 : 0;
        }

        return budgetDtos;
    }

    public async Task<BudgetDto> GetByIdAsync(int userId, int budgetId)
    {
        var budget = await _budgetRepository.GetByIdAsync(budgetId);
        if (budget == null || budget.UserId != userId)
            throw new Exception("Budget not found");

        var transactions = await _transactionRepository.GetByUserIdAndCategoryIdAsync(userId, budget.CategoryId);
        var totalSpent = transactions
            .Where(t => t.Type == TransactionType.Expense)
            .Sum(t => t.ConvertedAmount);

        var dto = _mapper.Map<BudgetDto>(budget);
        dto.SpentAmount = totalSpent;
        dto.RemainingAmount = budget.PlannedAmount - totalSpent;
        dto.SpentPercentage = budget.PlannedAmount > 0 ? (totalSpent / budget.PlannedAmount) * 100 : 0;

        return dto;
    }

    public async Task<BudgetSummaryDto> GetSummaryAsync(int userId, DateTime date)
    {
        var budgets = await _budgetRepository.GetByUserIdAsync(userId);
        var summary = new BudgetSummaryDto { Budgets = new List<BudgetDto>() };

        foreach (var budget in budgets)
        {
            if (date < budget.StartDate || date > budget.EndDate)
                continue;

            var transactions = await _transactionRepository.GetByUserIdAndCategoryIdAsync(userId, budget.CategoryId);
            var totalSpent = transactions
                .Where(t => t.Type == TransactionType.Expense && 
                       t.TransactionDate >= budget.StartDate && 
                       t.TransactionDate <= budget.EndDate)
                .Sum(t => t.ConvertedAmount);

            var dto = _mapper.Map<BudgetDto>(budget);
            dto.SpentAmount = totalSpent;
            dto.RemainingAmount = budget.PlannedAmount - totalSpent;
            dto.SpentPercentage = budget.PlannedAmount > 0 ? (totalSpent / budget.PlannedAmount) * 100 : 0;

            summary.Budgets.Add(dto);
        }

        return summary;
    }

    public async Task<BudgetDto> CreateAsync(int userId, CreateBudgetDto dto)
    {
        var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);
        if (category == null || category.UserId != userId)
            throw new Exception("Category not found");

        var existingBudget = await _budgetRepository.GetByUserIdAndCategoryAsync(userId, dto.CategoryId, dto.StartDate);
        if (existingBudget != null)
            throw new Exception("A budget already exists for this category and date range");

        var budget = _mapper.Map<Budget>(dto);
        budget.UserId = userId;

        await _budgetRepository.AddAsync(budget);
        return await GetByIdAsync(userId, budget.Id);
    }

    public async Task<BudgetDto> UpdateAsync(int userId, int budgetId, UpdateBudgetDto dto)
    {
        var budget = await _budgetRepository.GetByIdAsync(budgetId);
        if (budget == null || budget.UserId != userId)
            throw new Exception("Budget not found");

        _mapper.Map(dto, budget);
        await _budgetRepository.UpdateAsync(budget);

        return await GetByIdAsync(userId, budgetId);
    }

    public async Task DeleteAsync(int userId, int budgetId)
    {
        var budget = await _budgetRepository.GetByIdAsync(budgetId);
        if (budget == null || budget.UserId != userId)
            throw new Exception("Budget not found");

        await _budgetRepository.DeleteAsync(budgetId);
    }

} 