using CreateCustomer.API.Contracts;
using CreateCustomer.API.Entities;

namespace CreateCustomer.API.Repositories
{
    public class TaxScheduleRepository : GenericRepository<TaxSchedule>, ITaxScheduleRepository
    {
        public TaxScheduleRepository() : this(new CustomerContext())
        {

        }

        public TaxScheduleRepository(CustomerContext context) : base(context)
        {

        }
    }
}
