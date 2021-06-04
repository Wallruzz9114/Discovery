using System.Threading;
using System.Threading.Tasks;
using Core.Services.Interfaces;
using Data.Repositories.Interfaces;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Models.Entities;
using Core.Base.Queries;
using Core.Features.Customers.Queries.Authtenticate;
using Core.ViewModels;

namespace Core.Features.Customers.Queries.Authenticate
{
    public class AuthenticateCustomerQueryHandler : IQueryHandler<AuthenticateCustomerQuery, CustomerViewModel>
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ICustomerRepository _customerRepository;
        private readonly ITokenService _tokenService;

        public AuthenticateCustomerQueryHandler(SignInManager<User> signInManager, UserManager<User> userManager, ICustomerRepository customerRepository, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _customerRepository = customerRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<CustomerViewModel> Handle(AuthenticateCustomerQuery request, CancellationToken cancellationToken)
        {
            var customerViewModel = new CustomerViewModel();
            var signInAttempt = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, true);

            if (signInAttempt.Succeeded)
            {
                var token = await _tokenService.GenerateToken(request.Email);
                var user = await _userManager.FindByEmailAsync(request.Email);
                var customer = await _customerRepository.GetCustomerByEmail(user.Email, cancellationToken);

                customerViewModel.Id = customer.Id;
                customerViewModel.Name = customer.Name;
                customerViewModel.Email = user.Email;
                customerViewModel.Token = token;
                customerViewModel.LoginSucceeded = signInAttempt.Succeeded;
            }
            else
            {
                customerViewModel.ValidationResult.Errors
                    .Add(new ValidationFailure(string.Empty, "Username or password invalid"));
            }

            return customerViewModel;
        }
    }
}