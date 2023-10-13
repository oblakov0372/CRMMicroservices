using CRM.Deal.Service.Dtos;

namespace CRM.Deal.Service.DealManagement
{
  public interface IDealManagementService
  {
    Task<IEnumerable<DealDto>> GetAllDealsAsync();
    Task<IEnumerable<DealDto>> GetAllDealsByUserIdAsync(Guid creatorId);
    Task<DealDto> CreateDealAsync(CreateDealModel model, Guid creatorId);
    Task<bool> UpdateDealAsync(UpdateDealModel model, Guid Id, Guid UpdateById);
    Task<bool> DeleteDealAsync(Guid dealId);
  }
}