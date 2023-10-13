using CRM.Common;
using CRM.Deal.Service.Dtos;

namespace CRM.Deal.Service.DealManagement
{
  public class DealManagementService : IDealManagementService
  {
    private readonly IRepository<DealEntity> repository;
    public DealManagementService(IRepository<DealEntity> repository)
    {
      this.repository = repository;
    }

    public async Task<DealDto> CreateDealAsync(CreateDealModel model, Guid creatorId)
    {
      DealEntity dealEntity = new DealEntity()
      {
        Id = Guid.NewGuid(),
        CreatedById = creatorId,
        CreatedDate = DateTimeOffset.Now,
        Status = model.Status,
        TelegramUserId = model.TelegramUserId,
      };

      await repository.CreateAsync(dealEntity);

      return dealEntity.AsDto();
    }

    public async Task<bool> DeleteDealAsync(Guid dealId)
    {
      try
      {
        await repository.RemoveAsync(dealId);
      }
      catch (System.Exception)
      {
        return false;
      }
      return true;
    }

    public async Task<IEnumerable<DealDto>> GetAllDealsAsync()
    {
      var deals = await repository.GetAllAsync();
      return deals.Select(d => d.AsDto());
    }

    public async Task<IEnumerable<DealDto>> GetAllDealsByUserIdAsync(Guid creatorId)
    {
      var deals = await repository.GetAllAsync(d => d.CreatedById == creatorId);
      return deals.Select(d => d.AsDto());
    }

    public async Task<bool> UpdateDealAsync(UpdateDealModel model, Guid Id, Guid UpdateById)
    {
      DealEntity dealEntity = await repository.GetAsync(d => d.Id == Id);
      if (dealEntity == null)
        return false;
      dealEntity.Status = model.Status;
      dealEntity.UpdatedDate = DateTimeOffset.Now;
      dealEntity.UpdatedById = UpdateById;
      try
      {
        await repository.UpdateAsync(dealEntity);
      }
      catch
      {
        return false;
      }
      return true;
    }
  }
}