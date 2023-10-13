using CRM.Deal.Service;
using CRM.Deal.Service.Dtos;

namespace CRM.Deal
{
  public static class Extensions
  {
    public static DealDto AsDto(this DealEntity dealEntity)
    {
      return new DealDto(dealEntity.Id, dealEntity.Status, dealEntity.CreatedById, dealEntity.TelegramUserId, dealEntity.CreatedDate);
    }
  }
}