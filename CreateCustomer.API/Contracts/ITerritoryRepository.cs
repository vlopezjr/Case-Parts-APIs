using CreateCustomer.API.Entities;

namespace CreateCustomer.API.Contracts
{
    public interface ITerritoryRepository : IRepository<Territory>
    {
        string GetBranchIDByState(string state);
    }
}
