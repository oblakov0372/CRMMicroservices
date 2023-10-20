namespace CRM.Deal.Service.Dtos
{
  public record AccountDto(Guid Id, string UserName, string Email);
  public record DealDto
  (
    Guid Id,
    Status Status,
    Guid CreatedById,
    string CreatedByUserName,
    long TelegramUserId,
    DateTimeOffset CreatedDate
  );

  public record CreateDealModel
  (
    Status Status,
    long TelegramUserId
  );
  public record UpdateDealModel
  (
    Status Status
  );
}