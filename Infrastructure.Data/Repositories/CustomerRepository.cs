using Domain.Customers;

namespace Infrastructure.Data.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(IMongoContext context) : base(context)
        {
        }
    }
}
