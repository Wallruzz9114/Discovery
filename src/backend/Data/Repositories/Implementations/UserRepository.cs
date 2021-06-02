using System.Threading.Tasks;
using Data.Identity.Implementations;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Data.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityContext _dbContext;

        public UserRepository(IdentityContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}