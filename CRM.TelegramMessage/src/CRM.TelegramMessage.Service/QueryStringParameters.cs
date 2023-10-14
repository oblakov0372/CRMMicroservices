namespace CRM.TelegramMessage.Service
{
  public abstract class QueryStringParameters
  {
    const int maxPageSize = 100;
    public int PageNumber { get; set; } = 1;
    private int pageSize = 30;
    public int PageSize
    {
      get
      {
        return pageSize;
      }
      set
      {
        pageSize = (value > maxPageSize) ? maxPageSize : value;
      }
    }

    public string OrderBy { get; set; } = string.Empty;
    public string SearchQuery { get; set; } = string.Empty;
  }
}