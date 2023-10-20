using CRM.Deal.Service;
using CRM.TelegramMessage.Service;

namespace CRM.TelegramUser.Service
{
  public class DealParameters : QueryStringParameters
  {
    public DealParameters()
    {
      DealStatus = Status.None;
    }
    public Status DealStatus { get; set; }
  }
}
