using CRM.Common;
using CRM.TelegramUser.Service.Clients;
using CRM.TelegramUser.Service.Dtos;
using CRM.TelegramUser.Service.Entities;
namespace CRM.TelegramUser.Service.TelegramUserManagement
{
  public class TelegramUserManagementService : ITelegramUserManagementService
  {
    private readonly IRepository<TelegramUserEntity> repository;
    private readonly TelegramMessageClient telegramMessageClient;

    public TelegramUserManagementService(IRepository<TelegramUserEntity> repository, TelegramMessageClient telegramMessageClient)
    {
      this.repository = repository;
      this.telegramMessageClient = telegramMessageClient;
    }

    public async Task<(IEnumerable<TelegramUserLiteDto>, int)> GetAllTelegramUsersAsync(TelegramUsersParameters parameters)
    {
      var query = await repository.GetAllAsync();
      if (!string.IsNullOrEmpty(parameters.SearchQuery))
      {
        var byUsername = query
            .Where(u => u.TelegramUsername != null && u.TelegramUsername.ToLower().Contains(parameters.SearchQuery.ToLower()))
            .ToList();

        var byId = query
            .Where(u => u.Id.ToString().Contains(parameters.SearchQuery))
            .ToList();

        query = byUsername.Concat(byId).ToList();
      }

      var totalCount = query.Count();
      var totalPages = (int)Math.Ceiling((double)totalCount / parameters.PageSize);

      var telegramUsers = query

          .Skip((parameters.PageNumber - 1) * parameters.PageSize)
          .Take(parameters.PageSize)
          .ToList();
      return (telegramUsers.Select(tu => tu.AsDto()), totalPages);
    }
    public async Task<bool> EditUserStatusAsync(Guid id, Status status)
    {
      var existingUser = await repository.GetAsync(id);

      if (existingUser == null)
        return false;
      existingUser.Status = status;
      await repository.UpdateAsync(existingUser);
      return true;
    }

    public async Task<TelegramUserDto> GetDataForTelegramUserAsync(long telegramId)
    {
      var existingUser = await repository.GetAsync(u => u.TelegramId == telegramId);
      if (existingUser == null)
        return null;
      IEnumerable<TelegramMessageDto> telegramUserMessages = (await telegramMessageClient.GetUserTelegramMessages(telegramId));
      if (telegramUserMessages != null && telegramUserMessages.Any())
      {
        var telegramUserDto = new TelegramUserDto
        (
          existingUser.Id,
          telegramId,
          existingUser.TelegramUsername,
          existingUser.Status,
          telegramUserMessages.First().Date,
          telegramUserMessages.Last().Date,
          telegramUserMessages.Where(m => m.Type == "wtb").Count(),
          telegramUserMessages.Where(m => m.Type == "wts").Count(),
          telegramUserMessages.Count(),
          $"https://t.me/{existingUser.TelegramUsername}",
          telegramUserMessages.First().LinkForMessage
          );
        return telegramUserDto;
      }
      return null;
    }
  }
}