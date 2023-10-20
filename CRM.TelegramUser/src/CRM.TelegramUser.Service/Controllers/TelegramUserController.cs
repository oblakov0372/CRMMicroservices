using CRM.TelegramUser.Service.Clients;
using CRM.TelegramUser.Service.Dtos;
using CRM.TelegramUser.Service.Entities;
using CRM.TelegramUser.Service.TelegramUserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.TelegramUser.Service.Controllers
{
  [Controller]
  [Route("telegramUsers")]
  public class TelegramUserController : ControllerBase
  {
    private readonly ITelegramUserManagementService telegramUserManagementService;
    public TelegramUserController(ITelegramUserManagementService telegramUserManagementService)
    {
      this.telegramUserManagementService = telegramUserManagementService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] TelegramUsersParameters parameters)
    {
      (IEnumerable<TelegramUserLiteDto> telegramAccounts, var totalPages) = await telegramUserManagementService.GetAllTelegramUsersAsync(parameters);
      return Ok(new { telegramAccounts, totalPages });
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> EditTelegramUserStatusAsync([FromBody] Status status, long telegramUserId)
    {
      var editedAccount = await telegramUserManagementService.EditUserStatusAsync(telegramUserId, status);

      return Ok(editedAccount);
    }
    [HttpGet("GetTelegramUserData/{telegramId}")]
    public async Task<IActionResult> GetTelegramUserData(long telegramId)
    {
      TelegramUserDto userData = await telegramUserManagementService.GetDataForTelegramUserAsync(telegramId);
      if (userData == null)
        return BadRequest();

      return Ok(userData);
    }
  }
}