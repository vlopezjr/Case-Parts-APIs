using CreateCustomer.API.Entities;
using System.Collections.Generic;

namespace CreateCustomer.API.Contracts
{
    public interface ISalesSourceRepository : IRepository<SalesSource>
    {
        List<SalesSource> GetCPCSalesSources();
    }
}
