using CreateCustomer.API.Contracts;
using CreateCustomer.API.Entities;
using System.Linq;

namespace CreateCustomer.API.Repositories
{
    public class TerritoryRepository : GenericRepository<Territory>, ITerritoryRepository
    {
        public TerritoryRepository():this(new CustomerContext())
        {

        }

        public TerritoryRepository(CustomerContext context):base(context)
        {

        }

        public string GetBranchIDByState(string state)
        {
            return _context.Territories.FirstOrDefault(terr => terr.StateID == state).BranchID;
        }
    }
}
