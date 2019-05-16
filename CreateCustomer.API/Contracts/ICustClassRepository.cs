using CreateCustomer.API.Entities;

using System.Collections.Generic;


namespace CreateCustomer.API.Contracts
{
    public interface ICustClassRepository : IRepository<CustClass>, IAsyncRepository<CustClass>
    {
        ICollection<CustClass> GetAllByCompanyId();

    }
}
