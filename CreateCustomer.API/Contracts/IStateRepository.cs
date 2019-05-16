using CreateCustomer.API.Entities;
using System.Collections.Generic;

namespace CreateCustomer.API.Contracts
{
    public interface IStateRepository : IRepository<State>
    {
        List<string> GetStatesByCountryID(string countryId);
    }
}
