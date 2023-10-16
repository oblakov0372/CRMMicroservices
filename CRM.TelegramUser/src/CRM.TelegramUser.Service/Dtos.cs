using CRM.TelegramUser.Service.Entities;

namespace CRM.TelegramUser.Service.Dtos
{
  public record TelegramUserDto
  (
    Guid Id,
    long TelegramId,
    string UserName,
    Status Status,
    DateTime FirstActivity,
    DateTime LastActivity,
    int CountMessagesWtb,
    int CountMessagesWts,
    int CountAllMessages,
    string LinkToUserTelegram,
    string LinkToFirstMessage
  );
  public record TelegramUserLiteDto
  (
    Guid Id,
    long TelegramId,
    string UserName,
    Status Status
  );
  public record TelegramMessageDto
  (
    Guid Id,
    long TelegramGroupId,
    string TelegramGroupUsername,
    long SenderId,
    string SenderUsername,
    string Message,
    DateTime Date,
    string LinkForMessage,
    string Type
  );
  public record ResponseDataTelegramMessage(IEnumerable<TelegramMessageDto> TelegramMessages, int totalPages);
}