using CRM.Deal.Service.Dtos;

namespace CRM.Deal.Service.Client
{
  public class AccountClient
  {
    private readonly HttpClient httpClient;
    public AccountClient(HttpClient httpClient)
    {
      this.httpClient = httpClient;
    }
    public async Task<IEnumerable<AccountDto>> GetAccountsAsync()
    {
      IEnumerable<AccountDto> accounts = await httpClient.GetFromJsonAsync<IEnumerable<AccountDto>>("/accounts");
      return accounts;
    }
    public async Task<AccountDto> GetAccountAsync(Guid id)
    {
      AccountDto accounts = await httpClient.GetFromJsonAsync<AccountDto>($"/accounts/{id}");
      return accounts;
    }
  }
}