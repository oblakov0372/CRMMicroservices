using CRM.TelegramUser.Service.Dtos;

namespace CRM.TelegramUser.Service.Clients
{
  public class TelegramMessageClient
  {
    private readonly HttpClient httpClient;
    private readonly string baseUrl;
    public TelegramMessageClient(string baseUrl, HttpClient httpClient)
    {
      this.httpClient = httpClient;
      this.baseUrl = baseUrl;
    }
    public async Task<IEnumerable<TelegramMessageDto>> GetUserTelegramMessages(long telegramId)
    {
      var requestUri = $"{baseUrl}/telegramMessages/{telegramId}";
      ResponseDataTelegramMessage data = await httpClient.GetFromJsonAsync<ResponseDataTelegramMessage>(requestUri);
      return data.TelegramMessages;
    }
  }
}