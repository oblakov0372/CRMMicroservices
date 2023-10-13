namespace CRM.Deal.Service.Dtos
{
  public record DealDto
  (
    Guid Id,
    Status Status,
    Guid CreatedById,
    Guid TelegramUserId,
    DateTimeOffset CreatedDate
  );

  public record CreateDealModel
  (
    Status Status,
    Guid TelegramUserId
  );
  public record UpdateDealModel
  (
    Status Status
  );
}