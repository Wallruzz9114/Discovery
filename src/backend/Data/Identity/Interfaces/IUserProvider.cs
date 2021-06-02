using System;

namespace Data.Identity.Interfaces
{
    public interface IUserProvider
    {
        Guid GetUserId();
    }
}