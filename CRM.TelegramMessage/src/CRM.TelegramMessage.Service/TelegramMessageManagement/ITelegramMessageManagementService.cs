using CRM.TelegramMessage.Service.Entities;

namespace CRM.TelegramMessage.Service.TelegramMessageManagement
{
  public interface ITelegramMessageManagementService
  {
    Task<IEnumerable<TelegramMessageEntity>> GetAllTelegramMessagesAsync();
    Task<IEnumerable<TelegramMessageEntity>> GetTelegramMessagesBySenderIdAsync(long senderId);
  }
}