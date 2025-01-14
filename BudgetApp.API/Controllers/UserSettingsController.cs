namespace BudgetApp.API.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BudgetApp.API.Services;
using BudgetApp.API.DTOs.Settings;
using System.Security.Claims;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserSettingsController : ControllerBase
{
    private readonly IUserSettingsService _userSettingsService;

    public UserSettingsController(IUserSettingsService userSettingsService)
    {
        _userSettingsService = userSettingsService;
    }

    private int GetUserId()
    {
        return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }

    [HttpGet]
    public async Task<ActionResult<UserSettingsDto>> Get()
    {
        var userId = GetUserId();
        var settings = await _userSettingsService.GetByUserIdAsync(userId);
        if (settings == null)
            settings = await _userSettingsService.CreateDefaultSettingsAsync(userId);

        return Ok(settings);
    }

    [HttpPut]
    public async Task<ActionResult<UserSettingsDto>> Update(UpdateUserSettingsDto dto)
    {
        try
        {
            var userId = GetUserId();
            var settings = await _userSettingsService.UpdateAsync(userId, dto);
            return Ok(settings);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
} 