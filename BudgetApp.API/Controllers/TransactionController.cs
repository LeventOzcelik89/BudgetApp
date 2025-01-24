namespace BudgetApp.API.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BudgetApp.API.Services;
using BudgetApp.API.DTOs.Transaction;
using System.Security.Claims;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    private int GetUserId()
    {
        return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransactionDto>>> GetAll()
    {
        var userId = GetUserId();
        var transactions = await _transactionService.GetAllAsync(userId);
        return Ok(transactions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TransactionDto>> GetById(int id)
    {
        try
        {
            var userId = GetUserId();
            var transaction = await _transactionService.GetByIdAsync(userId, id);
            return Ok(transaction);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet("summary")]
    public async Task<ActionResult<TransactionSummaryDto>> GetSummary([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var userId = GetUserId();
        var summary = await _transactionService.GetSummaryAsync(userId, startDate, endDate);
        return Ok(summary);
    }

    [HttpPost]
    public async Task<ActionResult<TransactionDto>> Create(CreateTransactionDto dto)
    {
        try
        {
            var userId = GetUserId();
            var transaction = await _transactionService.CreateAsync(userId, dto);
            return CreatedAtAction(nameof(GetById), new { id = transaction.Id }, transaction);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TransactionDto>> Update(int id, UpdateTransactionDto dto)
    {
        try
        {
            var userId = GetUserId();
            var transaction = await _transactionService.UpdateAsync(userId, id, dto);
            return Ok(transaction);
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
            await _transactionService.DeleteAsync(userId, id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
} 