using CRM.Common;
using CRM.TelegramMessage.Service.Entities;

namespace CRM.TelegramMessage.Service.TelegramMessageManagement
{
  public class TelegramMessageManagementService : ITelegramMessageManagementService
  {
    private readonly IRepository<TelegramMessageEntity> repository;
    public TelegramMessageManagementService(IRepository<TelegramMessageEntity> repository)
    {
      this.repository = repository;
    }
    public async Task<IEnumerable<TelegramMessageEntity>> GetAllTelegramMessagesAsync()
    {
      var telegramMessages = await repository.GetAllAsync();
      return telegramMessages;
    }

    public async Task<IEnumerable<TelegramMessageEntity>> GetTelegramMessagesBySenderIdAsync(long senderId)
    {
      var telegramMessages = await repository.GetAllAsync(telegramMessage => telegramMessage.SenderId == senderId);
      return telegramMessages;
    }
  }

}