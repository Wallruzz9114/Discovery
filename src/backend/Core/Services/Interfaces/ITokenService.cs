using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken(string email);
    }
}