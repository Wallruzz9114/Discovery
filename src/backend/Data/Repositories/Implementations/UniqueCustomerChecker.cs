using Data.Repositories;
using Models.Interfaces;

namespace Models.Implementations
{
    public class UniqueCustomerChecker : IUniqueUCustomerChecker
    {
        private readonly IAppUnitOfWork _appUnitOfWork;

        public UniqueCustomerChecker(IAppUnitOfWork appUnitOfWork)
        {
            _appUnitOfWork = appUnitOfWork;
        }

        public bool IsUserUnique(string customerEmail)
        {
            var customer = _appUnitOfWork
                .CustomerRepository.GetCustomerByEmail(customerEmail).Result;

            return customer is null;
        }
    }
}