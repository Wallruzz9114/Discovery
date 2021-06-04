using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Base.Commands;
using Data.Repositories;
using Models.Base;
using Models.Interfaces;

namespace Core.Features.Customers.Commands.PlaceOrder
{
    public class PlaceOrderCommandHandler : CommandHandler<PlaceOrderCommand, CommandHandlerResult>
    {
        private readonly IAppUnitOfWork _appUnitOfWork;
        private readonly ICurrencyConverter _currencyConverter;

        public PlaceOrderCommandHandler(IAppUnitOfWork appUnitOfWork, ICurrencyConverter currencyConverter)
        {
            _currencyConverter = currencyConverter;
            _appUnitOfWork = appUnitOfWork;
        }

        public override async Task<Guid> RunCommand(PlaceOrderCommand command, CancellationToken cancellationToken)
        {
            var customer = await _appUnitOfWork.CustomerRepository
                .GetCustomerById(command.CustomerId, cancellationToken);
            var orderId = new Guid();

            if (customer is not null)
            {
                var productIds = command.Products.Select(p => p.Id).ToList();
                var products = await _appUnitOfWork.ProductRepository
                    .GetProductsByIds(productIds, cancellationToken);

                if (products.Count > 0)
                {
                    var basket = new Basket(command.Currency);

                    foreach (var product in products)
                    {
                        var quantity = command.Products.FirstOrDefault(p => p.Id == product.Id).Quantity;
                        basket.AddProduct(product.Id, product.Price, quantity);
                    }

                    orderId = customer.PlaceOrder(basket, _currencyConverter);

                    await _appUnitOfWork.CustomerRepository.AddCustomerOrders(customer);
                    await _appUnitOfWork.CommitAsync(cancellationToken);
                }
            }

            return orderId;
        }
    }
}