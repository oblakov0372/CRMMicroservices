using CRM.TelegramMessage.Service.DTOs;
using CRM.TelegramMessage.Service.TelegramMessageManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace CRM.TelegramMessage.Service.Controllers
{
  [Controller]
  [Route("telegramMessages")]
  [Authorize]
  public class TelegramMessageController : ControllerBase
  {
    private readonly ITelegramMessageManagementService telegramMessageManagement;
    public TelegramMessageController(ITelegramMessageManagementService telegramMessageManagement)
    {
      this.telegramMessageManagement = telegramMessageManagement;
    }
    [HttpGet("telegramMessages")]
    public async Task<IActionResult> GetAllTelegramMessagesAsync([FromQuery] TelegramMessagesParameters parameters)
    {
      (IEnumerable<TelegramMessageDto> telegramMessages, int totalPages) = await telegramMessageManagement.GetAllTelegramMessagesAsync(parameters);
      return Ok(new { telegramMessages, totalPages });
    }
    [HttpGet("telegramMessages/{senderId}")]
    public async Task<IActionResult> GetAllTelegramMessagesBySenderIdAsync(long senderId, [FromQuery] TelegramMessagesParameters parameters)
    {
      (IEnumerable<TelegramMessageDto> telegramMessages, int totalPages) = await telegramMessageManagement.GetTelegramMessagesBySenderIdAsync(senderId, parameters);
      return Ok(new { telegramMessages, totalPages });
    }
  }
}