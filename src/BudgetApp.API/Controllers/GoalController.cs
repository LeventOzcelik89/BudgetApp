namespace BudgetApp.API.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BudgetApp.API.Services;
using BudgetApp.API.DTOs.Goals;
using System.Security.Claims;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class GoalController : ControllerBase
{
    private readonly IGoalService _goalService;

    public GoalController(IGoalService goalService)
    {
        _goalService = goalService;
    }

    private int GetUserId()
    {
        return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GoalDto>>> GetAll()
    {
        var userId = GetUserId();
        var goals = await _goalService.GetAllAsync(userId);
        return Ok(goals);
    }

    [HttpGet("active")]
    public async Task<ActionResult<IEnumerable<GoalDto>>> GetActive()
    {
        var userId = GetUserId();
        var goals = await _goalService.GetActiveGoalsAsync(userId);
        return Ok(goals);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GoalDto>> GetById(int id)
    {
        try
        {
            var userId = GetUserId();
            var goal = await _goalService.GetByIdAsync(userId, id);
            return Ok(goal);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<GoalDto>> Create(CreateGoalDto dto)
    {
        try
        {
            var userId = GetUserId();
            var goal = await _goalService.CreateAsync(userId, dto);
            return CreatedAtAction(nameof(GetById), new { id = goal.Id }, goal);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<GoalDto>> Update(int id, UpdateGoalDto dto)
    {
        try
        {
            var userId = GetUserId();
            var goal = await _goalService.UpdateAsync(userId, id, dto);
            return Ok(goal);
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
            await _goalService.DeleteAsync(userId, id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}/progress")]
    public async Task<ActionResult> UpdateProgress(int id, [FromBody] decimal amount)
    {
        try
        {
            var userId = GetUserId();
            await _goalService.UpdateProgressAsync(userId, id, amount);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
} 