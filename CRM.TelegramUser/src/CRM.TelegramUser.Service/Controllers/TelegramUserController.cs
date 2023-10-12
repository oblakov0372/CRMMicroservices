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
    public async Task<IActionResult> GetAllAsync()
    {
      var telegramAccounts = await telegramUserManagementService.GetAllTelegramUsersAsync();
      return Ok(telegramAccounts);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> EditTelegramUserStatusAsync([FromBody] Status status, Guid id)
    {
      var editedAccount = await telegramUserManagementService.EditUserStatusAsync(id, status);

      return Ok(editedAccount);
    }
    [HttpGet("GetTelegramUserData/{Id}")]
    public async Task<IActionResult> GetTelegramUserData([FromBody] Guid Id)
    {
      TelegramUserDto userData = await telegramUserManagementService.GetDataForTelegramUserAsync(Id);
      if (userData == null)
        return BadRequest();

      return Ok(userData);
    }
  }
}