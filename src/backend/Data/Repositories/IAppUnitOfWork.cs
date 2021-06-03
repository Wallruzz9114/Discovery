using Data.Repositories.Interfaces;
using Models.Base;

namespace Data.Repositories
{
    public interface IAppUnitOfWork : IUnitOfWork
    {
        ICustomerRepository CustomerRepository { get; }
        IStoredEventRepository StoredEventRepository { get; }
        IProductRepository ProductRepository { get; }
    }
}