
namespace CRM.Deal.HttpHandler
{
  public class HttpTrackerHandler : DelegatingHandler
  {
    private readonly IHttpContextAccessor _context;
    public HttpTrackerHandler(IHttpContextAccessor context)
    {
      _context = context;
    }
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
      if (_context.HttpContext.Request.Headers.TryGetValue("Authorization", out var jwt))
        request.Headers.Add("Authorization", jwt.FirstOrDefault());

      return base.SendAsync(request, cancellationToken);
    }
  }
}