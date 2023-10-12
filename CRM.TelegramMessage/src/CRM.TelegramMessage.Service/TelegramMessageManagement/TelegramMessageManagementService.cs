using CRM.Common;
using CRM.TelegramMessage.Service.DTOs;
using CRM.TelegramMessage.Service.Entities;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CRM.TelegramMessage.Service.TelegramMessageManagement
{
  public class TelegramMessageManagementService : ITelegramMessageManagementService
  {
    private readonly IRepository<TelegramMessageEntity> repository;
    public TelegramMessageManagementService(IRepository<TelegramMessageEntity> repository)
    {
      this.repository = repository;
    }
    public async Task<IEnumerable<TelegramMessageDto>> GetAllTelegramMessagesAsync()
    {
      var telegramMessages = await repository.GetAllAsync();
      return telegramMessages.Select(tm => tm.AsDto());
    }

    public async Task<IEnumerable<TelegramMessageDto>> GetTelegramMessagesBySenderIdAsync(long senderId)
    {
      var telegramMessages = await repository.GetAllAsync(telegramMessage => telegramMessage.SenderId == senderId);
      return telegramMessages.Select(tm => tm.AsDto());
    }
  }

}