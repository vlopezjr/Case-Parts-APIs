using CreateCustomer.API.Contracts;
using CreateCustomer.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateCustomer.API.Repositories
{
    public class CustStatusRepository : GenericRepository<CustStatus>, ICustStatusRepository
    {
        public CustStatusRepository() : this(new CustomerContext()) { }

        public CustStatusRepository(CustomerContext context) : base(context)
        {
        }
    }
}
