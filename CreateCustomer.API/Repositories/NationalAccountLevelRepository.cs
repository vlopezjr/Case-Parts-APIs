using CreateCustomer.API.Contracts;
using CreateCustomer.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateCustomer.API.Repositories
{
    public class NationalAccountLevelRepository : GenericRepository<NationalAccountLevel>, INationalAccountLevelRepository
    {
        public NationalAccountLevelRepository():this(new CustomerContext())
        {

        }

        public NationalAccountLevelRepository(CustomerContext context) : base(context)
        {

        }

        public int GetUnunsedSubsidiaryAccountLevelKey(int nationalAcctKey)
        {
            return _context.NationalAccountLevels.First(c => c.NationalAcctKey == nationalAcctKey && c.NationalAcctLevel == 2).Key;
        }
    }
}
