using CRM.Common;

namespace CRM.Deal.Service
{
  public enum Status
  {
    Scamer,
    Reseller,
    InWork,
    HavedDeal
  }
  public class DealEntity : IEntity
  {
    public Guid Id { get; set; }
    public Status Status { get; set; }
    public Guid TelegramUserId { get; set; }
    public Guid CreatedById { get; set; }
    public Guid UpdatedById { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset UpdatedDate { get; set; }
  }
}