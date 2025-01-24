namespace BudgetApp.API.Services;

using AutoMapper;
using BudgetApp.API.Data.Repositories;
using BudgetApp.API.DTOs.Settings;
using BudgetApp.API.DTOs.Transaction;
using BudgetApp.API.Models;
using BudgetApp.API.Models.Enums;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUserSettingsRepository _userSettingsRepository;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly INotificationService _notificationService;
    private readonly IMapper _mapper;

    public TransactionService(
        ITransactionRepository transactionRepository,
        ICategoryRepository categoryRepository,
        IUserSettingsRepository userSettingsRepository,
        ICurrencyRepository currencyRepository,
        INotificationService notificationService,
        IMapper mapper)
    {
        _transactionRepository = transactionRepository;
        _categoryRepository = categoryRepository;
        _userSettingsRepository = userSettingsRepository;
        _currencyRepository = currencyRepository;
        _notificationService = notificationService;
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
            TotalIncome = transactions
                .Where(t => t.Type == TransactionType.Income)
                .Sum(t => t.ConvertedAmount),
            TotalExpense = transactions
                .Where(t => t.Type == TransactionType.Expense)
                .Sum(t => t.ConvertedAmount),
            CategorySummaries = new List<CategorySummaryDto>()
        };

        summary.Balance = summary.TotalIncome - summary.TotalExpense;

        // Kullanıcının para birimi bilgisini al
        var userSettings = await _userSettingsRepository.GetByUserIdAsync(userId);
        var currency = await _currencyRepository.GetByCodeAsync(userSettings.CurrencyCode);

        summary.Currency = _mapper.Map<CurrencyInfo>(currency);

        var categoryGroups = transactions
            .Where(t => t.CategoryId.HasValue)
            .GroupBy(t => t.Category);

        foreach (var group in categoryGroups)
        {
            var amount = group.Sum(t => t.ConvertedAmount);
            var percentage = amount / (group.First().Type == TransactionType.Income ? summary.TotalIncome : summary.TotalExpense) * 100;

            summary.CategorySummaries.Add(new CategorySummaryDto
            {
                CategoryId = group.Key.Id,
                CategoryName = group.Key.Name,
                Amount = amount,
                Percentage = percentage,
                Currency = summary.Currency
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

        // Kullanıcının varsayılan para birimini al
        var userSettings = await _userSettingsRepository.GetByUserIdAsync(userId);
        var userCurrencyCode = userSettings.CurrencyCode;

        // İşlem para birimini belirle
        var transactionCurrencyCode = dto.CurrencyCode ?? userCurrencyCode;
        var transactionCurrency = await _currencyRepository.GetByCodeAsync(transactionCurrencyCode);
        if (transactionCurrency == null || !transactionCurrency.IsActive)
            throw new Exception("Invalid currency code");

        // Döviz kurunu ve çevrilmiş tutarı hesapla
        decimal exchangeRate = 1;
        decimal convertedAmount = dto.Amount;

        if (transactionCurrencyCode != userCurrencyCode)
        {
            // TODO: Döviz kuru servisi entegrasyonu
            // exchangeRate = await _exchangeRateService.GetRateAsync(transactionCurrencyCode, userCurrencyCode);
            convertedAmount = dto.Amount * exchangeRate;
        }

        var transaction = new Transaction
        {
            UserId = userId,
            CategoryId = dto.CategoryId,
            Description = dto.Description,
            TransactionDate = dto.TransactionDate,
            Type = dto.Type,
            CurrencyCode = transactionCurrencyCode,
            OriginalAmount = dto.Amount,
            ConvertedAmount = convertedAmount,
            ExchangeRate = exchangeRate
        };

        await _transactionRepository.AddAsync(transaction);

        // Büyük işlem bildirimi
        if (convertedAmount >= userSettings.GetNotificationPreferences().LargeTransactionThreshold)
        {
            var categoryName = transaction.Category?.Name ?? "Uncategorized";
            await _notificationService.NotifyLargeTransactionAsync(userId, convertedAmount, categoryName);
        }

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

        // Kullanıcının varsayılan para birimini al
        var userSettings = await _userSettingsRepository.GetByUserIdAsync(userId);
        var userCurrencyCode = userSettings.CurrencyCode;

        // Döviz kurunu ve çevrilmiş tutarı hesapla
        decimal exchangeRate = 1;
        decimal convertedAmount = dto.Amount;

        if (transaction.CurrencyCode != userCurrencyCode)
        {
            // TODO: Döviz kuru servisi entegrasyonu
            // exchangeRate = await _exchangeRateService.GetRateAsync(transaction.CurrencyCode, userCurrencyCode);
            convertedAmount = dto.Amount * exchangeRate;
        }

        transaction.CategoryId = dto.CategoryId;
        transaction.Description = dto.Description;
        transaction.TransactionDate = dto.TransactionDate;
        transaction.Type = dto.Type;
        transaction.OriginalAmount = dto.Amount;
        transaction.ConvertedAmount = convertedAmount;
        transaction.ExchangeRate = exchangeRate;

        await _transactionRepository.UpdateAsync(transaction);

        // Büyük işlem bildirimi
        if (convertedAmount >= userSettings.GetNotificationPreferences().LargeTransactionThreshold)
        {
            var categoryName = transaction.Category?.Name ?? "Uncategorized";
            await _notificationService.NotifyLargeTransactionAsync(userId, convertedAmount, categoryName);
        }

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