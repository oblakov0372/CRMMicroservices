using CRM.Account.Dtos;
using CRM.Account.Entities;
using CRM.Account.JWT;
using CRM.Common;

namespace CRM.Account.AccountManagement
{
  public class AccountManagementService : IAccountManagementService
  {
    private readonly IRepository<AccountEntity> repository;
    private readonly IJwtAuthenticationManager jwtAuthenticationManager;
    public AccountManagementService(IRepository<AccountEntity> repository, IJwtAuthenticationManager jwtAuthenticationManager)
    {
      this.jwtAuthenticationManager = jwtAuthenticationManager;
      this.repository = repository;
    }

    private string HashPassword(string password)
    {
      string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

      return hashedPassword;
    }

    public async Task<AccountDto> RegisterAsync(CreateAccountDto registerModel)
    {
      var alreadyRegisteredAccount = await repository.GetAsync(acc => acc.Email == registerModel.Email ||
                                                           acc.UserName == registerModel.UserName);

      if (alreadyRegisteredAccount != null)
        return null;

      var hashedPassword = HashPassword(registerModel.Password);
      AccountEntity accountEntity = new AccountEntity
      {
        Id = Guid.NewGuid(),
        UserName = registerModel.UserName,
        Email = registerModel.Email,
        CreatedDate = DateTimeOffset.Now,
        PasswordHash = hashedPassword,
        Role = Role.User,
      };
      await repository.CreateAsync(accountEntity);

      return accountEntity.AsDto();
    }
    public async Task<string> LoginAsync(LoginAccountDto loginModel)
    {
      AccountEntity accountEntity = await repository.GetAsync(acc => acc.UserName == loginModel.UserName);

      if (accountEntity == null)
        return null;

      if (!BCrypt.Net.BCrypt.Verify(loginModel.Password, accountEntity.PasswordHash))
        return null;

      // If the password is verified, generate a JWT token.
      return jwtAuthenticationManager.GenerateToken(accountEntity);
    }

    public async Task<IEnumerable<AccountDto>> GetAllAsync()
    {
      var accounts = (await repository.GetAllAsync())
                     .Select(acc => acc.AsDto());
      return accounts;
    }

    public async Task<AccountDto> EditStatusAsync(Guid id, Role role)
    {
      AccountEntity existingAccount = await repository.GetAsync(id);
      existingAccount.Role = role;

      await repository.UpdateAsync(existingAccount);

      return existingAccount.AsDto();
    }
  }
}