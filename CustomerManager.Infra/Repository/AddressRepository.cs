using CustomerManager.Domain.Entities;
using CustomerManager.Domain.Interface.Repository;
using CustomerManager.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace CustomerManager.Infra.Repository
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(ApplicationDbContext context) : base(context) { }
    }
}
