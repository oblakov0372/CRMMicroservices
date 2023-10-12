using CRM.TelegramMessage.Service.DTOs;
using CRM.TelegramMessage.Service.Entities;

namespace CRM.TelegramMessage.Service
{
  public static class Extensions
  {
    public static TelegramMessageDto AsDto(this TelegramMessageEntity telegramMessageEntity)
    {
      return new TelegramMessageDto(telegramMessageEntity.Id, telegramMessageEntity.TelegramGroupId, telegramMessageEntity.TelegramGroupUsername, telegramMessageEntity.SenderId, telegramMessageEntity.SenderUsername, telegramMessageEntity.Message, telegramMessageEntity.Date, telegramMessageEntity.LinkForMessage, telegramMessageEntity.Type);
    }
  }
}