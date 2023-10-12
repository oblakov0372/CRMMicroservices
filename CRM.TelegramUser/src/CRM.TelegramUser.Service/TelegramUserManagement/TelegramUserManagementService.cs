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

    public async Task<IEnumerable<TelegramUserEntity>> GetAllTelegramUsersAsync()
    {
      var users = await repository.GetAllAsync();
      return users;
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

    public async Task<TelegramUserDto> GetDataForTelegramUserAsync(Guid Id)
    {
      var existingUser = await repository.GetAsync(Id);
      if (existingUser == null)
        return null;
      IEnumerable<TelegramMessageDto> telegramUserMessages = await telegramMessageClient.GetUserTelegramMessages(existingUser.TelegramId);
      if (telegramUserMessages != null && telegramUserMessages.Any())
      {
        var telegramUserDto = new TelegramUserDto
        (
          Id,
          existingUser.TelegramId,
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