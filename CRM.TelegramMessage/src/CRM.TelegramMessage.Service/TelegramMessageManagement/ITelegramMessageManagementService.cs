using CRM.TelegramMessage.Service.DTOs;
using CRM.TelegramMessage.Service.Entities;

namespace CRM.TelegramMessage.Service.TelegramMessageManagement
{
  public interface ITelegramMessageManagementService
  {
    Task<IEnumerable<TelegramMessageDto>> GetAllTelegramMessagesAsync();
    Task<IEnumerable<TelegramMessageDto>> GetTelegramMessagesBySenderIdAsync(long senderId);
  }
}