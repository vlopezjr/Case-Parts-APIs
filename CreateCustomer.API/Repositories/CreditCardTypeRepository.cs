using CreateCustomer.API.Contracts;
using CreateCustomer.API.Entities;

namespace CreateCustomer.API.Repositories
{
    public class CreditCardTypeRepository : GenericRepository<CreditCardType>, ICreditCardTypeRepository
    {
        public CreditCardTypeRepository() : this(new CustomerContext())
        {

        }

        public CreditCardTypeRepository(CustomerContext context) : base(context)
        {

        }
    }
}
