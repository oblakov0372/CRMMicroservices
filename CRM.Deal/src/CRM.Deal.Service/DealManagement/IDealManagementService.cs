using CRM.Deal.Service.Dtos;
using CRM.TelegramUser.Service;

namespace CRM.Deal.Service.DealManagement
{
  public interface IDealManagementService
  {
    Task<(IEnumerable<DealDto>, int)> GetAllDealsAsync(DealParameters parameters);
    Task<(IEnumerable<DealDto>, int)> GetAllDealsByUserIdAsync(Guid creatorId, DealParameters parameters);
    Task<DealDto> CreateDealAsync(CreateDealModel model, Guid creatorId);
    Task<bool> UpdateDealAsync(UpdateDealModel model, Guid Id, Guid UpdateById);
    Task<bool> DeleteDealAsync(Guid dealId);
  }
}