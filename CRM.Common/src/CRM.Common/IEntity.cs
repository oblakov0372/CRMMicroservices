namespace CRM.Common
{
  public interface IEntity
  {
    Guid Id { get; set; }
    Guid CreatedById { get; set; }
    Guid UpdatedById { get; set; }
    DateTimeOffset CreatedDate { get; set; }
    DateTimeOffset UpdatedDate { get; set; }
  }
}