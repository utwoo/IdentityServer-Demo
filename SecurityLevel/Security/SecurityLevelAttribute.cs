using Microsoft.AspNetCore.Authorization;

namespace SecurityLevel.Security
{
    public class SecurityLevelAttribute: AuthorizeAttribute
    {
        public SecurityLevelAttribute(int securityLevel)
        {
           Policy = $"{Constants.SECURITY_LEVEL}.{securityLevel}";
        }
    }
}