using CreateCustomer.API.Entities;

namespace CreateCustomer.API.Contracts
{
    public interface INationalAccountLevelRepository : IRepository<NationalAccountLevel>
    {
        int GetUnunsedSubsidiaryAccountLevelKey(int nationalAcctKey);
    }
}
