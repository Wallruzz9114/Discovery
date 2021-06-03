using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Repositories.Interfaces;
using Models.Base;
using Models.Interfaces;
using Models.Helpers;

namespace Data.Repositories
{
    public class AppUnitOfWork : UnitOfWork<AppDbContext>, IAppUnitOfWork
    {
        private readonly IEventSerializer _eventSerializer;
        public ICustomerRepository CustomerRepository { get; }
        public IStoredEventRepository StoredEventRepository { get; }
        public IProductRepository ProductRepository { get; }

        public AppUnitOfWork(AppDbContext dbContext, ICustomerRepository customerRepository, IStoredEventRepository storedEventRepository, IProductRepository productRepository, IEventSerializer eventSerializer) : base(dbContext)
        {
            CustomerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            StoredEventRepository = storedEventRepository ?? throw new ArgumentNullException(nameof(storedEventRepository));
            ProductRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _eventSerializer = eventSerializer ?? throw new ArgumentNullException(nameof(eventSerializer));
        }

        protected async override Task StoreEvents(CancellationToken cancellationToken)
        {
            var entities = DbContext.ChangeTracker.Entries()
                .Where(ee => ee.Entity is Entity e && e.EntityEvents != null)
                .Select(ee => ee.Entity as Entity)
                .ToArray();

            foreach (var entity in entities)
            {
                var events = entity.EntityEvents
                    .Select(entry => StoredEventHelper.BuildFromEntityEvent(entry, _eventSerializer))
                    .ToArray();

                entity.ClearEntityEvents();
                await DbContext.AddRangeAsync(events, cancellationToken);
            }
        }
    }
}