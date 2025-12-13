using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Grajciar.InternetBanking.WebAPI.Policies
{
    public class SelfHandler : AuthorizationHandler<SelfRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            SelfRequirement requirement
        )
        {
            var httpContext = context.Resource as HttpContext;
            if (httpContext == null)
                return Task.CompletedTask;

            if (!httpContext.User.Identity?.IsAuthenticated ?? true)
                return Task.CompletedTask;

            var routeId = httpContext.Request.RouteValues["id"]?.ToString();
            var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (routeId != null && routeId == userId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
