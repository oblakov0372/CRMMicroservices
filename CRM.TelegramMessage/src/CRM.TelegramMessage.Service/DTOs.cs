namespace CRM.TelegramMessage.Service.DTOs
{
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
}