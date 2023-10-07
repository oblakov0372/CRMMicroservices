using CRM.Account.Dtos;
using CRM.Account.Entities;

namespace CRM.Account
{
  public static class Extensions
  {
    public static AccountDto AsDto(this AccountEntity account)
    {
      return new AccountDto(account.Id, account.UserName, account.Role, account.Email);
    }
  }
}
