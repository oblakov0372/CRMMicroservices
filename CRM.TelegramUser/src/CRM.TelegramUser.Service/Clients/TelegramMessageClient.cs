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
      IEnumerable<TelegramMessageDto> telegramMessages = await httpClient.GetFromJsonAsync<IEnumerable<TelegramMessageDto>>(requestUri);
      return telegramMessages;
    }
  }
}