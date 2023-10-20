using CRM.Common;
using CRM.Deal.Service.Client;
using CRM.Deal.Service.Dtos;
using CRM.TelegramUser.Service;

namespace CRM.Deal.Service.DealManagement
{
  public class DealManagementService : IDealManagementService
  {
    private readonly IRepository<DealEntity> repository;
    private readonly AccountClient accountHttpClient;
    public DealManagementService(IRepository<DealEntity> repository, AccountClient accountHttpClient)
    {
      this.accountHttpClient = accountHttpClient;
      this.repository = repository;
    }
    public async Task<(IEnumerable<DealDto>, int)> GetAllDealsAsync(DealParameters parameters)
    {
      var query = await repository.GetAllAsync();

      if (parameters.DealStatus != Status.None)
      {
        query = query.Where(deal => deal.Status == parameters.DealStatus).ToList();
      }

      if (!string.IsNullOrEmpty(parameters.SearchQuery))
      {
        query = query.Where(deal => deal.TelegramUserId.ToString().ToLower().Contains(parameters.SearchQuery.ToLower())).ToList();
      }

      var totalCount = query.Count();
      var totalPages = (int)Math.Ceiling((double)totalCount / parameters.PageSize);

      var deals = query
          .OrderBy(deal => deal.CreatedDate)
          .Skip((parameters.PageNumber - 1) * parameters.PageSize)
          .Take(parameters.PageSize);

      var accounts = await accountHttpClient.GetAccountsAsync();
      var dealsDto = deals.Select(deal =>
      {
        var createdByAccount = accounts.Single(account => account.Id == deal.CreatedById);
        return deal.AsDto(createdByAccount.UserName);
      });

      return (dealsDto, totalPages);
    }

    public async Task<(IEnumerable<DealDto>, int)> GetAllDealsByUserIdAsync(Guid creatorId, DealParameters parameters)
    {
      var query = await repository.GetAllAsync(deal => deal.CreatedById == creatorId);

      if (parameters.DealStatus != Status.None)
      {
        query = query.Where(deal => deal.Status == parameters.DealStatus).ToList();
      }

      if (!string.IsNullOrEmpty(parameters.SearchQuery))
      {
        query = query.Where(deal => deal.TelegramUserId.ToString().ToLower().Contains(parameters.SearchQuery.ToLower())).ToList();
      }

      var totalCount = query.Count();
      var totalPages = (int)Math.Ceiling((double)totalCount / parameters.PageSize);

      var deals = query
          .OrderBy(deal => deal.CreatedDate) // Change the property used for sorting as needed
          .Skip((parameters.PageNumber - 1) * parameters.PageSize)
          .Take(parameters.PageSize);

      var account = await accountHttpClient.GetAccountAsync(creatorId);
      var dealsDto = deals.Select(deal => deal.AsDto(account.UserName));

      return (dealsDto, totalPages);
    }

    public async Task<DealDto> CreateDealAsync(CreateDealModel model, Guid creatorId)
    {
      var account = await accountHttpClient.GetAccountAsync(creatorId);
      DealEntity dealEntity = new DealEntity()
      {
        Id = Guid.NewGuid(),
        CreatedById = creatorId,
        CreatedDate = DateTimeOffset.Now,
        Status = model.Status,
        TelegramUserId = model.TelegramUserId
      };

      await repository.CreateAsync(dealEntity);

      return dealEntity.AsDto(account.UserName);
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
    public async Task<bool> UpdateDealAsync(UpdateDealModel model, Guid Id, Guid updateById)
    {
      DealEntity dealEntity = await repository.GetAsync(d => d.Id == Id);
      if (dealEntity == null)
        return false;
      dealEntity.Status = model.Status;
      dealEntity.UpdatedDate = DateTimeOffset.Now;
      dealEntity.UpdatedById = updateById;
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