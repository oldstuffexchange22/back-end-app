using Microsoft.AspNetCore.Authorization;
using old_stuff_exchange_v2.Enum.Role;
using System.Security.Claims;
using System.Threading.Tasks;

namespace old_stuff_exchange_v2.Authorize
{
    public class ResidentRequirement : AuthorizationHandler<ResidentRequirement>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResidentRequirement requirement)
        {
            if (context.User.HasClaim(ClaimTypes.Role, RoleNames.RESIDENT))
            {
                context.Succeed(requirement);
            }
            return Task.FromResult(0);
        }
    }
}
