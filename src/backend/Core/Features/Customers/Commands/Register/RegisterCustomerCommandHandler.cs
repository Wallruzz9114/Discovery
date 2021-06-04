using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Core.Base.Commands;
using Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Models.Entities;
using Models.Interfaces;

namespace Core.Features.Customers.Commands.Register
{
    public class RegisterCustomerCommandHandler : CommandHandler<RegisterCustomerCommand, CommandHandlerResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly IAppUnitOfWork _appUnitOfWork;
        private readonly IUniqueUCustomerChecker _uniqueUCustomerChecker;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RegisterCustomerCommandHandler(UserManager<User> userManager, IAppUnitOfWork appUnitOfWork, IUniqueUCustomerChecker uniqueUCustomerChecker, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _uniqueUCustomerChecker = uniqueUCustomerChecker;
            _appUnitOfWork = appUnitOfWork;
            _userManager = userManager;
        }

        public override async Task<Guid> RunCommand(RegisterCustomerCommand command, CancellationToken cancellationToken)
        {
            var customer = Customer.CreateCustomer(command.Email, command.Name, _uniqueUCustomerChecker);

            if (customer is not null)
            {
                await _appUnitOfWork.CustomerRepository.RegisterCustomer(customer, cancellationToken);
                if (await _appUnitOfWork.CommitAsync(cancellationToken))
                    await CreateUserForCustomer(command);
            }

            return customer.Id;
        }

        private async Task<User> CreateUserForCustomer(RegisterCustomerCommand command)
        {
            var user = new User(_httpContextAccessor)
            {
                UserName = command.Name,
                Email = command.Email
            };
            var userCreated = await _userManager.CreateAsync(user, command.Password);

            if (userCreated.Succeeded)
            {
                await _userManager.AddClaimAsync(user, new Claim("CanRead", "Read"));
                await _userManager.AddClaimAsync(user, new Claim("CanSave", "Save"));
                await _userManager.AddClaimAsync(user, new Claim("CanDelete", "Delete"));
            }

            return user;
        }
    }
}