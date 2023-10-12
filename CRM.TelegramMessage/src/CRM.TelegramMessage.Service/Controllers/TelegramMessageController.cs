using CRM.TelegramMessage.Service.TelegramMessageManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace CRM.TelegramMessage.Service.Controllers
{

  [Controller]
  [Route("telegramMessages")]
  public class TelegramMessageController : ControllerBase
  {
    private readonly ITelegramMessageManagementService telegramMessageManagement;
    public TelegramMessageController(ITelegramMessageManagementService telegramMessageManagement)
    {
      this.telegramMessageManagement = telegramMessageManagement;
    }
    [HttpGet("telegramMessages")]
    public async Task<IActionResult> GetAllTelegramMessagesAsync()
    {
      var telegramMessages = await telegramMessageManagement.GetAllTelegramMessagesAsync();
      return Ok(telegramMessages);
    }
    [HttpGet("telegramMessages/{senderId}")]
    public async Task<IActionResult> GetAllTelegramMessagesBySenderIdAsync(long senderId)
    {
      var telegramMessages = await telegramMessageManagement.GetTelegramMessagesBySenderIdAsync(senderId);
      return Ok(telegramMessages);
    }
  }
}