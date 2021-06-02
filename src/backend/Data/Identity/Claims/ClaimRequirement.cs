using Microsoft.AspNetCore.Authorization;

namespace Data.Identity.Claims
{
    public class ClaimRequirement : IAuthorizationRequirement
    {
        public string ClaimName { get; set; }
        public string ClaimValue { get; set; }
    }
}