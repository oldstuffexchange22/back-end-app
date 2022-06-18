using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace old_stuff_exchange_v2.Enum.Authorize
{
    public static class Operations
    {
        public static OperationAuthorizationRequirement Create =
            new OperationAuthorizationRequirement { Name = nameof(Create) };
        public static OperationAuthorizationRequirement Read =
            new OperationAuthorizationRequirement { Name = nameof(Read) };
        public static OperationAuthorizationRequirement Update =
            new OperationAuthorizationRequirement { Name = nameof(Update) };
        public static OperationAuthorizationRequirement Delete =
            new OperationAuthorizationRequirement { Name = nameof(Delete) };
        public static OperationAuthorizationRequirement Dilivered =
            new OperationAuthorizationRequirement { Name = nameof(Dilivered) };
    }
}
