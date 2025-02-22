using CustomerManager.Domain.Entities;

namespace CustomerManager.Domain.Interface.Repository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> GetByEmailAsync(string email);
    }
}
