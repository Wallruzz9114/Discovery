using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Models.Base;

namespace Models.Entities
{
    public class User : IdentityUser<Guid>, IUser
    {
        public string Name => GetName();

        private readonly IHttpContextAccessor _httpContextAccessor;

        private User() { }

        public User(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsAuthenticated()
        {
            return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _httpContextAccessor.HttpContext.User.Claims;
        }

        private string GetName()
        {
            return _httpContextAccessor.HttpContext.User.Identity.Name
                ?? _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        }
    }
}