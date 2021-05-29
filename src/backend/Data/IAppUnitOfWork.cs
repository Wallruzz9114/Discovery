using Data.Repositories.Interfaces;
using Models.Base;

namespace Data
{
    public interface IAppUnitOfWork : IUnitOfWork
    {
        ICustomerRepository CustomerRepository { get; }
        IEventRepository EventRepository { get; }
        IProductRepository ProductRepository { get; }
    }
}