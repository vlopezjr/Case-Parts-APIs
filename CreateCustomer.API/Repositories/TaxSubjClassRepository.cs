using CreateCustomer.API.Contracts;
using CreateCustomer.API.Entities;

namespace CreateCustomer.API.Repositories
{
    public class TaxSubjClassRepository : GenericRepository<TaxSubjClass>, ITaxSubjClassRepository
    {
        public TaxSubjClassRepository() :this(new CustomerContext())
        {

        }

        public TaxSubjClassRepository(CustomerContext context): base(context)
        {

        }
    }
}
