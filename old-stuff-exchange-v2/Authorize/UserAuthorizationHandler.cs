using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using old_stuff_exchange_v2.Entities;
using old_stuff_exchange_v2.Enum.Authorize;
using old_stuff_exchange_v2.Enum.Role;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace old_stuff_exchange_v2.Authorize
{
    public class UserAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, User>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, User resource)
        {
            var permissions = new List<UserPermissionType>();
            if (context.User.HasClaim(ClaimTypes.Role, RoleNames.ADMIN))
            {
                context.Succeed(requirement);
                return Task.FromResult(0);
            }

            if (context.User.HasClaim("id", Convert.ToString(resource.Id)))
            {
                permissions.Add(UserPermissionType.Owner);
            }
            if (ValidateUserPermissions[requirement](permissions))
            {
                context.Succeed(requirement);
            }
            return Task.FromResult(0);
        }

        static readonly Dictionary<OperationAuthorizationRequirement, Func<List<UserPermissionType>, bool>> ValidateUserPermissions
            = new Dictionary<OperationAuthorizationRequirement, Func<List<UserPermissionType>, bool>>

    {
        { Operations.Create, x => x.Contains(UserPermissionType.Creator) ||
                                  x.Contains(UserPermissionType.Owner)  },

        { Operations.Read, x => x.Contains(UserPermissionType.Creator) ||
                                x.Contains(UserPermissionType.Reader) ||
                                x.Contains(UserPermissionType.Contributor) ||
                                x.Contains(UserPermissionType.Owner) },

        { Operations.Update, x => x.Contains(UserPermissionType.Contributor) ||
                                x.Contains(UserPermissionType.Owner) },

        { Operations.Delete, x => x.Contains(UserPermissionType.Owner) },
    };

        private enum UserPermissionType { Admin, Creator, Reader, Contributor, Owner };
    }

    
}
