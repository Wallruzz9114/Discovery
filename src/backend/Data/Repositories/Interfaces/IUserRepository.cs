using System.Threading.Tasks;
using Models.Entities;

namespace Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmail(string email);
    }
}