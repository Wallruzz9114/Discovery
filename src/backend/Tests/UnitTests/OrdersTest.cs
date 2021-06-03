using System.Linq;
using Models.Base;
using Models.Entities;
using Models.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Tests.UnitTests
{
    [TestFixture]
    public class OrdersTests
    {
        const string name = "Customer";
        const string email = "test@email.com";

        [Test]
        public void Order_Cannot_Be_Placed_Without_Products()
        {
            var checker = Substitute.For<IUniqueUCustomerChecker>();
            var currencyConverter = Substitute.For<ICurrencyConverter>();
            checker.IsUserUnique(email).Returns(true);
            var customer = Customer.CreateCustomer(email, name, checker);
            Basket cart = new(Currency.USD.Name);

            var businessRuleValidationException = Assert.Catch<BusinessException>(() =>
            {
                customer.PlaceOrder(cart, currencyConverter);
            });

            Assert.True(customer.Orders.Count == 0);
        }

        [Test]
        public void Order_Has_Orderlines()
        {
            var baseCurrency = Currency.USD;
            var checker = Substitute.For<IUniqueUCustomerChecker>();
            checker.IsUserUnique(email).Returns(true);

            var currencyConverter = Substitute.For<ICurrencyConverter>();
            currencyConverter.GetBaseCurrency().Returns(baseCurrency);

            var customer = Customer.CreateCustomer(email, name, checker);

            Product product1 = new("Product 1", Money.Of((decimal)1.50, baseCurrency.Name));
            Product product2 = new("Product 2", Money.Of((decimal)4.00, baseCurrency.Name));

            Basket basket = new(Currency.USD.Name);
            basket.AddProduct(product1.Id, product1.Price, 1);
            basket.AddProduct(product2.Id, product1.Price, 1);

            customer.PlaceOrder(basket, currencyConverter);
            var orderLines = customer.Orders.SelectMany(order => order.OrderLines).ToList();

            Assert.True(orderLines.Count == 2);
        }
    }
}