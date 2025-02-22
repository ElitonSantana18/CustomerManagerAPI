using CustomerManager.Domain.Entities;
using CustomerManager.Domain.Interface.Repository;
using CustomerManager.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace CustomerManager.Infra.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Customer> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.Email == email);
        }
    }
}
