using CRM.Account.Entities;

namespace CRM.Account.Dtos
{
  public record AccountDto(Guid Id, string UserName, Role Role, string Email);
  public record CreateAccountDto(string UserName, string Email, string Password);
  public record UpdateAccountDto(string UserName, string Email, string Password);
  public record UpdateAccountRole(Role role);
  public record LoginAccountDto(string UserName, string Password);

}