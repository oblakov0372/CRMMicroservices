using CRM.TelegramMessage.Service.TelegramMessageManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace CRM.TelegramMessage.Service.Controllers
{

  [Controller]
  [Microsoft.AspNetCore.Components.Route("telegramMessages")]
  [Authorize]
  public class TelegramMessageController : ControllerBase
  {
    private readonly ITelegramMessageManagementService telegramMessageManagement;
    public TelegramMessageController(ITelegramMessageManagementService telegramMessageManagement)
    {
      this.telegramMessageManagement = telegramMessageManagement;
    }
    [HttpGet("GetAllTelegramMessages")]
    public async Task<IActionResult> GetAllTelegramMessagesAsync()
    {
      var telegramMessages = await telegramMessageManagement.GetAllTelegramMessagesAsync();
      return Ok(telegramMessages);
    }
    [HttpGet("senderId")]
    public async Task<IActionResult> GetAllTelegramMessagesBySenderIdAsync([FromBody] long senderId)
    {
      var telegramMessages = await telegramMessageManagement.GetTelegramMessagesBySenderIdAsync(senderId);
      return Ok(telegramMessages);
    }
  }
}