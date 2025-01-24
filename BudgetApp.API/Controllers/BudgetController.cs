namespace BudgetApp.API.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BudgetApp.API.Services;
using BudgetApp.API.DTOs.Budget;
using System.Security.Claims;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BudgetController : ControllerBase
{
    private readonly IBudgetService _budgetService;

    public BudgetController(IBudgetService budgetService)
    {
        _budgetService = budgetService;
    }

    private int GetUserId()
    {
        return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BudgetDto>>> GetAll()
    {
        var userId = GetUserId();
        var budgets = await _budgetService.GetAllAsync(userId);
        return Ok(budgets);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BudgetDto>> GetById(int id)
    {
        try
        {
            var userId = GetUserId();
            var budget = await _budgetService.GetByIdAsync(userId, id);
            return Ok(budget);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet("summary")]
    public async Task<ActionResult<BudgetSummaryDto>> GetSummary([FromQuery] DateTime date)
    {
        var userId = GetUserId();
        var summary = await _budgetService.GetSummaryAsync(userId, date);
        return Ok(summary);
    }

    [HttpPost]
    public async Task<ActionResult<BudgetDto>> Create(CreateBudgetDto dto)
    {
        try
        {
            var userId = GetUserId();
            var budget = await _budgetService.CreateAsync(userId, dto);
            return CreatedAtAction(nameof(GetById), new { id = budget.Id }, budget);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<BudgetDto>> Update(int id, UpdateBudgetDto dto)
    {
        try
        {
            var userId = GetUserId();
            var budget = await _budgetService.UpdateAsync(userId, id, dto);
            return Ok(budget);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var userId = GetUserId();
            await _budgetService.DeleteAsync(userId, id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
} 