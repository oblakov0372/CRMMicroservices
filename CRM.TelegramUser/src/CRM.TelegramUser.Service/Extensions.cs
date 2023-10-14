using CRM.TelegramUser.Service.Dtos;
using CRM.TelegramUser.Service.Entities;

namespace CRM.TelegramUser.Service
{
  public static class Extensions
  {
    public static TelegramUserLiteDto AsDto(this TelegramUserEntity entity)
    {
      return new TelegramUserLiteDto(entity.Id, entity.TelegramId, entity.TelegramUsername, entity.Status);
    }
  }
}