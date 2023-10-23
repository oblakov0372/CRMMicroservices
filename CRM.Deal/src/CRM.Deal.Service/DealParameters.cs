using CRM.Common;
using CRM.Deal.Service;

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
