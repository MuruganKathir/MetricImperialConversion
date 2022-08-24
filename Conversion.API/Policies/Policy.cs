using Microsoft.AspNetCore.Authorization;

namespace Conversion.API.Policies
{
    public static class Policy
    {
        public static AuthorizationPolicy AdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(UserRoles.Admin).Build();
        }

        public static AuthorizationPolicy UserPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(UserRoles.User).Build();
        }
    }
}