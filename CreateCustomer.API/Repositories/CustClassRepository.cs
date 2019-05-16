using CreateCustomer.API.Contracts;
using CreateCustomer.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateCustomer.API.Repositories
{
    public class CustClassRepository : GenericRepository<CustClass>, ICustClassRepository
    {
        public CustClassRepository() : this(new CustomerContext()) { }

        public CustClassRepository(CustomerContext context) : base(context)
        {
        }

        public ICollection<CustClass> GetAllByCompanyId()
        {
            return _context.Set<CustClass>().Where(c => c.CompanyId == "CPC").ToList();
        }
    }
}
