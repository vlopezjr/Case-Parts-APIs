using CreateCustomer.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateCustomer.API.Contracts
{
    public interface ICustomerRepository : IRepository<Customer>, IAsyncRepository<Customer>
    {
        void AddCustomerWithDependecies(Customer customer);

        void UpdateCustomerWithDependecies(Customer customer);

        Customer GetWithDependenciesById(string id);

        Customer GetWithDependenciesByKey(int key);

        Task<List<string>> GetCustIdsAsync();

        void UpdateHQNonPrimaryContactDocTransmittal(int key, int tranType, bool action, int contactKey);

        void UpdateHQPrimaryDocTransmittal(int key, int tranType, bool action);

        Tuple<string, string> SearchCustIdByString(string searchString);

        Task<List<string>> GetStandAloneCustIDs();

        Task<List<string>> GetCustIdsWithQualifiersAsync();
    }
}
