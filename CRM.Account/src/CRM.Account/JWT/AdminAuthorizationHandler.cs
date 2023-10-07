using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CRM.Account
{
  public class AdminAuthorizationHandler : AuthorizationHandler<AdminRequirement>
  {
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
    {
      if (context.User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "Admin"))
      {
        context.Succeed(requirement);
      }

      return Task.CompletedTask;
    }
  }
  public class AdminRequirement : IAuthorizationRequirement
  {
  }
}