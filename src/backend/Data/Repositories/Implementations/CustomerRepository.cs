using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Data.Repositories.Implementations
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _dbContext;

        public CustomerRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task AddCustomerOrders(Customer customer)
        {
            if (customer.Orders.Count > 0)
            {
                var storedCustomer = await GetUntrackedCustomerById(customer.Id);

                foreach (var order in customer.Orders)
                {
                    var customerFromDb = storedCustomer.Orders.Find(ol => ol.Id == order.Id);

                    if (customerFromDb is null)
                    {
                        _dbContext.Orders.Add(order);
                        _dbContext.OrderLines.AddRange(order.OrderLines);
                    }
                }
            }
        }

        public async Task ChangeCustomerOrder(Customer customer, Guid orderId)
        {
            var order = customer.Orders.Single(o => o.Id == orderId);

            if (order is not null)
            {
                await SaveOrderLines(customer, orderId);
                _dbContext.Orders.Update(order);
            }
        }

        public async Task<Customer> GetCustomerByEmail(string email, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Customers.Where(c => c.Email == email).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Customer> GetCustomerById(Guid id, CancellationToken cancellationToken = default)
        {
            var customer = await _dbContext.Customers
                .Where(c => c.Id == id)
                .Include(c => c.Orders)
                .FirstOrDefaultAsync(cancellationToken);

            return customer;
        }

        public async Task RegisterCustomer(Customer customer, CancellationToken cancellationToken = default)
        {
            await _dbContext.Customers.AddAsync(customer, cancellationToken);
        }

        public void UpdateCustomer(Customer customer)
        {
            _dbContext.Customers.Update(customer);
        }

        public async Task SaveOrderLines(Customer customer, Guid orderId)
        {
            var storedCustomer = await GetUntrackedCustomerById(customer.Id);
            var storedCustomerOrder = storedCustomer.Orders.Single(o => o.Id == orderId);
            var customerOrderLines = customer.Orders.SelectMany(o => o.OrderLines).Where(o => o.OrderId == orderId).ToList();

            foreach (var orderLine in customerOrderLines)
            {
                var orderLineFromDb = storedCustomerOrder.OrderLines.Find(ol => ol.Id == orderLine.Id);

                if (orderLineFromDb is null) _dbContext.OrderLines.Add(orderLine);
                else _dbContext.OrderLines.Update(orderLineFromDb);
            }
        }

        private async Task<Customer> GetUntrackedCustomerById(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Customers.AsNoTracking()
                .Where(c => c.Id == id)
                .Include(c => c.Orders)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}