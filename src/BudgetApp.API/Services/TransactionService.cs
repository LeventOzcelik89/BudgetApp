namespace BudgetApp.API.Services;

using AutoMapper;
using BudgetApp.API.Data.Repositories;
using BudgetApp.API.DTOs.Transaction;
using BudgetApp.API.Models;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public TransactionService(
        ITransactionRepository transactionRepository,
        ICategoryRepository categoryRepository,
        IMapper mapper)
    {
        _transactionRepository = transactionRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TransactionDto>> GetAllAsync(int userId)
    {
        var transactions = await _transactionRepository.GetByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<TransactionDto>>(transactions);
    }

    public async Task<TransactionDto> GetByIdAsync(int userId, int transactionId)
    {
        var transaction = await _transactionRepository.GetByIdAsync(transactionId);
        if (transaction == null || transaction.UserId != userId)
            throw new Exception("Transaction not found");

        return _mapper.Map<TransactionDto>(transaction);
    }

    public async Task<IEnumerable<TransactionDto>> GetByDateRangeAsync(int userId, DateTime startDate, DateTime endDate)
    {
        var transactions = await _transactionRepository.GetByUserIdAndDateRangeAsync(userId, startDate, endDate);
        return _mapper.Map<IEnumerable<TransactionDto>>(transactions);
    }

    public async Task<TransactionSummaryDto> GetSummaryAsync(int userId, DateTime startDate, DateTime endDate)
    {
        var transactions = await _transactionRepository.GetByUserIdAndDateRangeAsync(userId, startDate, endDate);
        
        var summary = new TransactionSummaryDto
        {
            TotalIncome = transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount),
            TotalExpense = transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount),
            CategorySummaries = new List<CategorySummaryDto>()
        };

        summary.Balance = summary.TotalIncome - summary.TotalExpense;

        var categoryGroups = transactions
            .Where(t => t.CategoryId.HasValue)
            .GroupBy(t => t.Category);

        foreach (var group in categoryGroups)
        {
            var amount = group.Sum(t => t.Amount);
            var percentage = amount / (group.First().Type == TransactionType.Income ? summary.TotalIncome : summary.TotalExpense) * 100;

            summary.CategorySummaries.Add(new CategorySummaryDto
            {
                CategoryId = group.Key.Id,
                CategoryName = group.Key.Name,
                Amount = amount,
                Percentage = percentage
            });
        }

        return summary;
    }

    public async Task<TransactionDto> CreateAsync(int userId, CreateTransactionDto dto)
    {
        if (dto.CategoryId.HasValue)
        {
            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId.Value);
            if (category == null || category.UserId != userId)
                throw new Exception("Category not found");
        }

        var transaction = _mapper.Map<Transaction>(dto);
        transaction.UserId = userId;

        await _transactionRepository.AddAsync(transaction);
        return _mapper.Map<TransactionDto>(transaction);
    }

    public async Task<TransactionDto> UpdateAsync(int userId, int transactionId, UpdateTransactionDto dto)
    {
        var transaction = await _transactionRepository.GetByIdAsync(transactionId);
        if (transaction == null || transaction.UserId != userId)
            throw new Exception("Transaction not found");

        if (dto.CategoryId.HasValue)
        {
            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId.Value);
            if (category == null || category.UserId != userId)
                throw new Exception("Category not found");
        }

        _mapper.Map(dto, transaction);
        await _transactionRepository.UpdateAsync(transaction);

        return _mapper.Map<TransactionDto>(transaction);
    }

    public async Task DeleteAsync(int userId, int transactionId)
    {
        var transaction = await _transactionRepository.GetByIdAsync(transactionId);
        if (transaction == null || transaction.UserId != userId)
            throw new Exception("Transaction not found");

        await _transactionRepository.DeleteAsync(transactionId);
    }
} 