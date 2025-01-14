namespace BudgetApp.API.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BudgetApp.API.Services;
using BudgetApp.API.DTOs.Currency;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CurrencyController : ControllerBase
{
    private readonly ICurrencyService _currencyService;

    public CurrencyController(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CurrencyDto>>> GetAll()
    {
        var currencies = await _currencyService.GetAllAsync();
        return Ok(currencies);
    }

    [HttpGet("active")]
    public async Task<ActionResult<IEnumerable<CurrencyDto>>> GetActive()
    {
        var currencies = await _currencyService.GetActiveCurrenciesAsync();
        return Ok(currencies);
    }

    [HttpGet("{code}")]
    public async Task<ActionResult<CurrencyDto>> GetByCode(string code)
    {
        var currency = await _currencyService.GetByCodeAsync(code);
        if (currency == null)
            return NotFound();

        return Ok(currency);
    }

    [HttpPut("{code}")]
    [Authorize(Roles = "Admin")] // Sadece admin kullanıcılar güncelleyebilir
    public async Task<ActionResult<CurrencyDto>> Update(string code, UpdateCurrencyDto dto)
    {
        try
        {
            var currency = await _currencyService.UpdateAsync(code, dto);
            return Ok(currency);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
} 