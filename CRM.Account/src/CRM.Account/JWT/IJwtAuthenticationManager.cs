
using CRM.Account.Entities;

namespace CRM.Account.JWT
{
  public interface IJwtAuthenticationManager
  {
    string GenerateToken(AccountEntity user);
  }
}
