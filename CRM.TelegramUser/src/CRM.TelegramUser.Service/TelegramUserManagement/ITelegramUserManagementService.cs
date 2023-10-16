using CRM.TelegramUser.Service.Dtos;
using CRM.TelegramUser.Service.Entities;

namespace CRM.TelegramUser.Service.TelegramUserManagement
{
  public interface ITelegramUserManagementService
  {
    Task<(IEnumerable<TelegramUserLiteDto>, int)> GetAllTelegramUsersAsync(TelegramUsersParameters parameters);
    Task<bool> EditUserStatusAsync(Guid id, Status status);
    Task<TelegramUserDto> GetDataForTelegramUserAsync(long Id);
  }
}