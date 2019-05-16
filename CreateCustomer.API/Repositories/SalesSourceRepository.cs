using CreateCustomer.API.Contracts;
using CreateCustomer.API.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CreateCustomer.API.Repositories
{
    public class SalesSourceRepository : GenericRepository<SalesSource>, ISalesSourceRepository
    {
        public SalesSourceRepository():this(new CustomerContext())
        {

        }

        public SalesSourceRepository(CustomerContext context) : base(context)
        {

        }

        public List<SalesSource> GetCPCSalesSources()
        {
            return _context.SalesSources.Where(c => c.CompanyID == "CPC").ToList();
        }
    }
}
