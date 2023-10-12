using CRM.Common;

namespace CRM.TelegramUser.Service.Entities
{
  public enum Status
  {
    None,
    Scamer,
    Reseller,
    InWork
  }
  public class TelegramUserEntity : IEntity
  {
    public Guid Id { get; set; }
    public long TelegramId { get; set; }
    public string TelegramUsername { get; set; }
    public Status Status { get; set; }
    public Guid CreatedById { get; set; }
    public Guid UpdatedById { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset UpdatedDate { get; set; }
  }
}