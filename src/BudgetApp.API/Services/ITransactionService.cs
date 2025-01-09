namespace BudgetApp.API.Services;

using BudgetApp.API.DTOs.Transaction;

public interface ITransactionService
{
    Task<IEnumerable<TransactionDto>> GetAllAsync(int userId);
    Task<TransactionDto> GetByIdAsync(int userId, int transactionId);
    Task<IEnumerable<TransactionDto>> GetByDateRangeAsync(int userId, DateTime startDate, DateTime endDate);
    Task<TransactionSummaryDto> GetSummaryAsync(int userId, DateTime startDate, DateTime endDate);
    Task<TransactionDto> CreateAsync(int userId, CreateTransactionDto dto);
    Task<TransactionDto> UpdateAsync(int userId, int transactionId, UpdateTransactionDto dto);
    Task DeleteAsync(int userId, int transactionId);
} 