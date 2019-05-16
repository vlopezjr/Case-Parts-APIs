using CreateCustomer.API.Contracts;
using CreateCustomer.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateCustomer.API.Repositories
{
    public class StatementCycleRepository : GenericRepository<StatementCycle>, IStatementCycleRepository
    {
        public StatementCycleRepository() : this(new CustomerContext()) { }

        public StatementCycleRepository(CustomerContext context) : base(context)
        {
        }

        public ICollection<StatementCycle> GetAllByCompanyId()
        {
            return _context.Set<StatementCycle>().Where(c => c.CompanyId == "CPC").ToList();
        }   
    }
}
