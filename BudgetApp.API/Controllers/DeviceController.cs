namespace BudgetApp.API.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BudgetApp.API.Services;
using BudgetApp.API.DTOs.Devices;
using System.Security.Claims;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DeviceController : ControllerBase
{
    private readonly IDeviceService _deviceService;

    public DeviceController(IDeviceService deviceService)
    {
        _deviceService = deviceService;
    }

    private int GetUserId()
    {
        return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DeviceDto>>> GetAll()
    {
        var userId = GetUserId();
        var devices = await _deviceService.GetAllAsync(userId);
        return Ok(devices);
    }

    [HttpPost("register")]
    public async Task<ActionResult<DeviceDto>> RegisterDevice(RegisterDeviceDto dto)
    {
        try
        {
            var userId = GetUserId();
            var device = await _deviceService.RegisterDeviceAsync(userId, dto);
            return Ok(device);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{token}")]
    public async Task<ActionResult<DeviceDto>> UpdateDevice(string token, UpdateDeviceDto dto)
    {
        try
        {
            var userId = GetUserId();
            var device = await _deviceService.UpdateDeviceAsync(userId, token, dto);
            return Ok(device);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{token}")]
    public async Task<ActionResult> DeactivateDevice(string token)
    {
        try
        {
            var userId = GetUserId();
            await _deviceService.DeactivateDeviceAsync(userId, token);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
} 