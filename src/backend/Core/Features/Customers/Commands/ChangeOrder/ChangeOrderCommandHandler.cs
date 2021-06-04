using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Base.Commands;
using Core.Features.Customers.Commands.ChangeOrder;
using Data.Repositories;
using Models.Base;
using Models.Interfaces;

namespace Core.Features.Customers.Commands.ChangeOrder
{
    public class ChangeOrderCommandHandler : CommandHandler<ChangeOrderCommand, CommandHandlerResult>
    {
        private readonly IAppUnitOfWork _appUnitOfWork;
        private readonly ICurrencyConverter _currencyConverter;

        public ChangeOrderCommandHandler(IAppUnitOfWork appUnitOfWork, ICurrencyConverter currencyConverter)
        {
            _currencyConverter = currencyConverter;
            _appUnitOfWork = appUnitOfWork;
        }

        public override async Task<Guid> RunCommand(ChangeOrderCommand command, CancellationToken cancellationToken)
        {
            var customer = await _appUnitOfWork.CustomerRepository.GetCustomerById(command.CustomerId, cancellationToken);

            if (customer is not null)
            {
                var productIds = command.Products.Select(p => p.Id).ToList();
                var products = await _appUnitOfWork.ProductRepository.GetProductsByIds(productIds, cancellationToken);

                if (products.Count > 0)
                {
                    var basket = new Basket(command.Currency);

                    foreach (var product in products)
                    {
                        var quantity = command.Products.Where(p => p.Id == product.Id).FirstOrDefault().Quantity;
                        basket.AddProduct(product.Id, product.Price, quantity);
                    }

                    customer.ChangeOrder(basket, command.OrderId, _currencyConverter);
                    await _appUnitOfWork.CustomerRepository.ChangeCustomerOrder(customer, command.OrderId);
                    await _appUnitOfWork.CommitAsync(cancellationToken);
                }
            }

            return command.OrderId;
        }
    }
}