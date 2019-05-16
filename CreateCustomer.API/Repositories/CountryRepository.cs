using CreateCustomer.API.Contracts;
using CreateCustomer.API.Entities;

namespace CreateCustomer.API.Repositories
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        public CountryRepository() : this(new CustomerContext())
        {

        }

        public CountryRepository(CustomerContext context) : base (context)
        {

        }
    }
}
