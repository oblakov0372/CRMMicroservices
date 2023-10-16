using System.Net.Http.Json;
using CRM.TelegramUser.Service.Dtos;
using CRM.TelegramUser.Service.Entities;

namespace CRM.TelegramUser.Service.Clients
{
  public class TelegramMessageClient
  {
    private readonly HttpClient httpClient;
    public TelegramMessageClient(HttpClient httpClient)
    {
      this.httpClient = httpClient;
    }
    public async Task<IEnumerable<TelegramMessageDto>> GetUserTelegramMessages(long telegramId)
    {
      var requestUri = $"/telegramMessages/{telegramId}";
      ResponseDataTelegramMessage data = await httpClient.GetFromJsonAsync<ResponseDataTelegramMessage>(requestUri);
      return data.TelegramMessages;
    }
  }
}