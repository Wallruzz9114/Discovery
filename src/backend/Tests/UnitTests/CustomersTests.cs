using Models.Base;
using Models.Entities;
using Models.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Tests.UnitTests
{
    [TestFixture]
    public class CustomersTests
    {
        const string name = "Customer";
        const string email = "test@email.com";

        [Test]
        public void Is_Customer_Email_Unique()
        {
            var checker = Substitute.For<IUniqueUCustomerChecker>();
            checker.IsUserUnique(email).Returns(true);
            var customer = Customer.CreateCustomer(email, name, checker);
            Assert.True(customer.Email == email);
        }

        [Test]
        public void Is_Customer_Email_Already_In_Use()
        {
            Customer customer = null;
            var checker = Substitute.For<IUniqueUCustomerChecker>();
            checker.IsUserUnique(email).Returns(false);
            var businessException = Assert.Catch<BusinessException>(() =>
            {
                customer = Customer.CreateCustomer(email, name, checker);
            });

            Assert.IsNull(customer);
        }
    }
}