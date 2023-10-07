using CRM.Account.Dtos;
using CRM.Account.Entities;

namespace CRM.Account.AccountManagement
{
  public interface IAccountManagementService
  {
    Task<IEnumerable<AccountDto>> GetAllAsync();
    Task<AccountDto> EditStatusAsync(Guid id, Role role);
    Task<AccountDto> RegisterAsync(CreateAccountDto createModel);
    Task<string> LoginAsync(LoginAccountDto loginModel);

  }
}