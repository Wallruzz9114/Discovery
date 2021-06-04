using System.Threading;
using System.Threading.Tasks;
using Data.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Models.Events.Customers;

namespace Core.Features.Customers.Commands.Register
{
    public class RegisteredCustomerEventHandler : INotificationHandler<CustomerCreatedEvent>
    {
        private readonly IAppUnitOfWork _appUnitOfWork;
        private readonly IServiceScopeFactory _scopeFactory;

        public RegisteredCustomerEventHandler(IAppUnitOfWork appUnitOfWork, IServiceScopeFactory scopeFactory)
        {
            _appUnitOfWork = appUnitOfWork;
            _scopeFactory = scopeFactory;
        }

        public async Task Handle(CustomerCreatedEvent notification, CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();
            var customer = await _appUnitOfWork.CustomerRepository
                .GetCustomerById(notification.AggregateId, cancellationToken);

            if (customer is not null)
            {
                customer.SetWelcomeEmailSent(true);
                _appUnitOfWork.CustomerRepository.UpdateCustomer(customer);
                await _appUnitOfWork.CommitAsync(cancellationToken);
            }
        }
    }
}