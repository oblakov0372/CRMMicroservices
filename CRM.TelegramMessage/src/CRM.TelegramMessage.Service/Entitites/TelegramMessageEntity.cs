using CRM.Common;

namespace CRM.TelegramMessage.Service.Entities
{
  public class TelegramMessageEntity : IEntity
  {
    public Guid Id { get; set; }
    public long TelegramGroupId { get; set; }
    public string TelegramGroupUsername { get; set; }
    public long SenderId { get; set; }
    public string SenderUsername { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string LinkForMessage { get; set; }
    public string Type { get; set; }
    public Guid CreatedById { get; set; }
    public Guid UpdatedById { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset UpdatedDate { get; set; }
  }
}