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
    public class ProductAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Product>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Product resource)
        {
            var permissions = new List<UserPermissionType>();
            if (context.User.HasClaim(ClaimTypes.Role, RoleNames.ADMIN))
            {
                context.Succeed(requirement);
                return Task.FromResult(0);
            }
            if (context.User.HasClaim("id", Convert.ToString(resource.Post.AuthorId)))
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
        { Operations.Create, x => x.Contains(UserPermissionType.Owner) ||
                                      x.Contains(UserPermissionType.Admin)},
        { Operations.Read, x => x.Contains(UserPermissionType.Owner) ||
                                    x.Contains(UserPermissionType.Admin)},
        { Operations.Update, x => x.Contains(UserPermissionType.Admin) ||
                                x.Contains(UserPermissionType.Owner) },

    };

        private enum UserPermissionType { Admin, Owner };
    }
}

