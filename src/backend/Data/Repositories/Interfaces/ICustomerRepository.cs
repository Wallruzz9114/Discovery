using System;
using System.Threading;
using System.Threading.Tasks;
using Models.Base;
using Models.Entities;

namespace Data.Repositories.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task RegisterCustomer(Customer customer, CancellationToken cancellationToken = default);
        Task<Customer> GetCustomerById(Guid id, CancellationToken cancellationToken = default);
        Task<Customer> GetCustomerByEmail(string email, CancellationToken cancellationToken = default);
        void UpdateCustomer(Customer customer);
        Task AddCustomerOrders(Customer customer);
        Task ChangeCustomerOrder(Customer customer, Guid orderId);
    }
}