using System.Collections.Generic;
using System.Security.Claims;

namespace Models.Base
{
    public interface IUser
    {
        string Name { get; }
        string Email { get; }
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
    }
}