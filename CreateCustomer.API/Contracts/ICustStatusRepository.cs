using CreateCustomer.API.Entities;

namespace CreateCustomer.API.Contracts
{
    public interface ICustStatusRepository : IRepository<CustStatus>, IAsyncRepository<CustStatus>
    {
    }
}
