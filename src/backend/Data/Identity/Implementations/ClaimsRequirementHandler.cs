using System.Linq;
using System.Threading.Tasks;
using Data.Identity.Claims;
using Microsoft.AspNetCore.Authorization;

namespace src.backend.Data.Identity.Implementations
{
    public class ClaimsRequirementHandler : AuthorizationHandler<ClaimRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimRequirement requirement)
        {
            var claim = context.User.Claims
                .FirstOrDefault(c => c.Type == requirement.ClaimName);

            if (claim != null && claim.Value.Contains(requirement.ClaimValue))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}