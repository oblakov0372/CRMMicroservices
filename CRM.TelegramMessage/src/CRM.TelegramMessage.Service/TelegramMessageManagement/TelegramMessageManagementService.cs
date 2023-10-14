using CRM.Common;
using CRM.TelegramMessage.Service.DTOs;
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
    public async Task<(IEnumerable<TelegramMessageDto>, int)> GetAllTelegramMessagesAsync(TelegramMessagesParameters parameters)
    {
      var query = await repository.GetAllAsync();
      if (!string.Equals(parameters.MessageType, "all", StringComparison.OrdinalIgnoreCase))
      {
        query = query.Where(m => m.Type == parameters.MessageType).ToList();
      }

      if (!string.IsNullOrEmpty(parameters.SearchQuery))
      {
        query = query.Where(m => m.Message.Contains(parameters.SearchQuery)).ToList();
      }

      var totalCount = query.Count();
      var totalPages = (int)Math.Ceiling((double)totalCount / parameters.PageSize);

      var telegramMessages = query
          .OrderByDescending(m => m.Date)
          .Skip((parameters.PageNumber - 1) * parameters.PageSize)
          .Take(parameters.PageSize);

      return (telegramMessages.Select(tm => tm.AsDto()), totalPages);
    }

    public async Task<(IEnumerable<TelegramMessageDto>, int)> GetTelegramMessagesBySenderIdAsync(long senderId, TelegramMessagesParameters parameters)
    {
      var query = await repository.GetAllAsync(telegramMessage => telegramMessage.SenderId == senderId);
      if (!string.Equals(parameters.MessageType, "all", StringComparison.OrdinalIgnoreCase))
      {
        query = query.Where(m => m.Type == parameters.MessageType).ToList();
      }

      if (!string.IsNullOrEmpty(parameters.SearchQuery))
      {
        query = query.Where(m => m.Message.Contains(parameters.SearchQuery)).ToList();
      }

      var totalCount = query.Count();
      var totalPages = (int)Math.Ceiling((double)totalCount / parameters.PageSize);

      var telegramMessages = query
          .OrderByDescending(m => m.Date)
          .Skip((parameters.PageNumber - 1) * parameters.PageSize)
          .Take(parameters.PageSize);

      return (telegramMessages.Select(tm => tm.AsDto()), totalPages);
    }
  }

}