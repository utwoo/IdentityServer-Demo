using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace SecurityLevel.Security
{
    public class SecurityLevelPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        public SecurityLevelPolicyProvider(IOptions<AuthorizationOptions> options)
            : base(options)
        {
        }

        public override Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (!policyName.StartsWith(Constants.SECURITY_LEVEL)) return base.GetPolicyAsync(policyName);

            var policyParts = policyName.Split('.');
            var level = Convert.ToInt32(policyParts.Last());

            var authorizationPolicy = new AuthorizationPolicyBuilder()
                .AddRequirements(new SecurityLevelAuthorizationRequirement(level))
                .Build();

            return Task.FromResult(authorizationPolicy);
        }
    }
}