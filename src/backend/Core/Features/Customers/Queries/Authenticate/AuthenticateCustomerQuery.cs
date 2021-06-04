using Core.Base.Queries;
using Core.ViewModels;

namespace Core.Features.Customers.Queries.Authtenticate
{
    public class AuthenticateCustomerQuery : IQuery<CustomerViewModel>
    {
        public AuthenticateCustomerQuery(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}