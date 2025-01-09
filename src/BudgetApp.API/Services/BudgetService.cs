namespace BudgetApp.API.Services;

using AutoMapper;
using BudgetApp.API.Data.Repositories;
using BudgetApp.API.DTOs.Budget;
using BudgetApp.API.Models;

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
            var transactions = await _transactionRepository.GetByUserIdAndCategoryAsync(userId, budgetDto.CategoryId);
            budgetDto.ActualAmount = transactions
                .Where(t => t.TransactionDate >= budgetDto.StartDate && t.TransactionDate <= budgetDto.EndDate)
                .Sum(t => t.Amount);
        }

        return budgetDtos;
    }

    public async Task<BudgetDto> GetByIdAsync(int userId, int budgetId)
    {
        var budget = await _budgetRepository.GetByIdAsync(budgetId);
        if (budget == null || budget.UserId != userId)
            throw new Exception("Budget not found");

        var budgetDto = _mapper.Map<BudgetDto>(budget);
        var transactions = await _transactionRepository.GetByUserIdAndCategoryAsync(userId, budget.CategoryId);
        budgetDto.ActualAmount = transactions
            .Where(t => t.TransactionDate >= budget.StartDate && t.TransactionDate <= budget.EndDate)
            .Sum(t => t.Amount);

        return budgetDto;
    }

    public async Task<BudgetSummaryDto> GetSummaryAsync(int userId, DateTime date)
    {
        var activeBudgets = await _budgetRepository.GetActiveBudgetsAsync(userId, date);
        var budgetDtos = new List<BudgetDto>();

        foreach (var budget in activeBudgets)
        {
            var budgetDto = _mapper.Map<BudgetDto>(budget);
            var transactions = await _transactionRepository.GetByUserIdAndCategoryAsync(userId, budget.CategoryId);
            budgetDto.ActualAmount = transactions
                .Where(t => t.TransactionDate >= budget.StartDate && t.TransactionDate <= budget.EndDate)
                .Sum(t => t.Amount);
            budgetDtos.Add(budgetDto);
        }

        return new BudgetSummaryDto
        {
            TotalPlannedAmount = budgetDtos.Sum(b => b.PlannedAmount),
            TotalActualAmount = budgetDtos.Sum(b => b.ActualAmount),
            Budgets = budgetDtos
        };
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