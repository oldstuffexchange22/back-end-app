using Microsoft.AspNetCore.Authorization;
using old_stuff_exchange_v2.Enum.Role;
using System.Security.Claims;
using System.Threading.Tasks;

namespace old_stuff_exchange_v2.Authorize
{
    public class AdminRequirement : AuthorizationHandler<AdminRequirement>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
        {
            if (context.User.HasClaim(ClaimTypes.Role, RoleNames.ADMIN)
               )
            {
                context.Succeed(requirement);
            }
            return Task.FromResult(0);
        }
    }
}
