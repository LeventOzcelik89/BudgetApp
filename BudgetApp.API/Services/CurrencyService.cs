namespace BudgetApp.API.Services;

using AutoMapper;
using BudgetApp.API.Data.Repositories;
using BudgetApp.API.DTOs.Currency;
using BudgetApp.API.Models;

public class CurrencyService : ICurrencyService
{
    private readonly ICurrencyRepository _currencyRepository;
    private readonly IMapper _mapper;

    public CurrencyService(ICurrencyRepository currencyRepository, IMapper mapper)
    {
        _currencyRepository = currencyRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CurrencyDto>> GetAllAsync()
    {
        var currencies = await _currencyRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CurrencyDto>>(currencies);
    }

    public async Task<IEnumerable<CurrencyDto>> GetActiveCurrenciesAsync()
    {
        var currencies = await _currencyRepository.GetActiveCurrenciesAsync();
        return _mapper.Map<IEnumerable<CurrencyDto>>(currencies);
    }

    public async Task<CurrencyDto> GetByCodeAsync(string code)
    {
        var currency = await _currencyRepository.GetByCodeAsync(code);
        return _mapper.Map<CurrencyDto>(currency);
    }

    public async Task<CurrencyDto> UpdateAsync(string code, UpdateCurrencyDto dto)
    {
        var currency = await _currencyRepository.GetByCodeAsync(code);
        if (currency == null)
            throw new Exception("Currency not found");

        currency.IsActive = dto.IsActive;
        currency.DisplayOrder = dto.DisplayOrder;

        await _currencyRepository.UpdateAsync(currency);
        return _mapper.Map<CurrencyDto>(currency);
    }
} 