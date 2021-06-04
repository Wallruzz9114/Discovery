using System;
using System.Collections.Generic;
using System.Linq;
using Models.Base;
using Models.Events.Customers;
using Models.Events.Orders;
using Models.Interfaces;

namespace Models.Entities
{
    public class Customer : Entity, IAggregateRoot
    {
        private Customer() { }

        private Customer(string email, string name)
        {
            Email = email;
            Name = name;
            WelcomeEmailReceived = false;
            AddEntityEvent(new CustomerCreatedEvent(Id, name, email));
        }

        public string Email { get; private set; }
        public string Name { get; private set; }
        public bool WelcomeEmailReceived { get; private set; }
        public readonly List<Order> Orders = new();

        public static Customer CreateCustomer(string email, string name, IUniqueUCustomerChecker customerChecker)
        {
            if (!customerChecker.IsUserUnique(email))
            {
                throw new BusinessException("This email is already in use.");
            }

            return new Customer(email, name);
        }

        public Guid PlaceOrder(Basket basket, ICurrencyConverter currencyConverter)
        {
            if (!basket.Products.Any())
            {
                throw new BusinessException("An order should have at least one product.");
            }

            Order order = new(basket, currencyConverter);
            Orders.Add(order);
            AddEntityEvent(new OrderPlacedEvent(Id, order.Id));

            return order.Id;
        }

        public Guid ChangeOrder(Basket basket, Guid orderId, ICurrencyConverter currencyConverter)
        {
            if (!basket.Products.Any())
            {
                throw new BusinessException("An order should have at least one product.");
            }

            var orderToChange = Orders.Single(o => o.Id == orderId);
            orderToChange.Change(basket, currencyConverter);

            AddEntityEvent(new OrderChangedEvent(orderToChange.Id));
            return orderToChange.Id;
        }

        public void SetName(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));
            Name = value;
        }

        public void SetWelcomeEmailSent(bool value) => WelcomeEmailReceived = value;
    }
}