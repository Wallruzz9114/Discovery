using System;
using System.Linq;
using System.Security.Claims;
using Data.Identity.Interfaces;
using Microsoft.AspNetCore.Http;

namespace src.backend.Data.Identity.Implementations
{
    public class UserProvider : IUserProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public Guid GetUserId()
        {
            var userIdString = _httpContextAccessor.HttpContext.User.Claims
                .First(i => i.Type == ClaimTypes.NameIdentifier).Value;

            _ = Guid.TryParse(userIdString, out Guid userId);

            return userId;
        }
    }
}