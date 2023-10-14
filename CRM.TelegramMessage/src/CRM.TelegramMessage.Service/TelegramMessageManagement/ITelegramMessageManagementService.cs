using CRM.TelegramMessage.Service.DTOs;
using CRM.TelegramMessage.Service.Entities;

namespace CRM.TelegramMessage.Service.TelegramMessageManagement
{
  public interface ITelegramMessageManagementService
  {
    Task<(IEnumerable<TelegramMessageDto>, int)> GetAllTelegramMessagesAsync(TelegramMessagesParameters parameters);
    Task<(IEnumerable<TelegramMessageDto>, int)> GetTelegramMessagesBySenderIdAsync(long senderId, TelegramMessagesParameters parameters);
  }
}