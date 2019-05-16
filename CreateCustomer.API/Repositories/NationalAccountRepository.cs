using CreateCustomer.API.Contracts;
using CreateCustomer.API.Entities;

namespace CreateCustomer.API.Repositories
{
    public class NationalAccountRepository : GenericRepository<NationalAccount>, INationalAccountRepository
    {
        public NationalAccountRepository() : this(new CustomerContext())
        {

        }
        public NationalAccountRepository(CustomerContext context) : base(context)
        {

        }
    }
}
