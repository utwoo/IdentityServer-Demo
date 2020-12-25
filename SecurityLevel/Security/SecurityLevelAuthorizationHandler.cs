using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SecurityLevel.Security
{
    public class SecurityLevelAuthorizationRequirement : IAuthorizationRequirement
    {
        public readonly int SecurityLevel;

        public SecurityLevelAuthorizationRequirement(int securityLevel)
        {
            SecurityLevel = securityLevel;
        }
    }

    public class SecurityLevelAuthorizationHandler : AuthorizationHandler<SecurityLevelAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            SecurityLevelAuthorizationRequirement requirement)
        {
            var claimValue =
                Convert.ToInt32(
                    context.User.Claims.FirstOrDefault(claim => claim.Type == Constants.SECURITY_LEVEL)?.Value ?? "0");
            if (requirement.SecurityLevel <= claimValue)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            
            return Task.CompletedTask;
        }
    }
}