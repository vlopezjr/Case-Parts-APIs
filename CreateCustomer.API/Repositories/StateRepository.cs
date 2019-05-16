using CreateCustomer.API.Contracts;
using CreateCustomer.API.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CreateCustomer.API.Repositories
{
    public class StateRepository : GenericRepository<State>, IStateRepository
    {
        public StateRepository() : this(new CustomerContext())
        {

        }

        public StateRepository(CustomerContext context) : base(context)
        {

        }


        public List<string> GetStatesByCountryID(string countryId)
        {
            return _context.States
                        .Where(c => c.CountryID.Contains(countryId))
                        .Select(c => c.StateID).ToList();
        }
    }
}
