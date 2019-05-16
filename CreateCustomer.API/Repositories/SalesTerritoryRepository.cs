using CreateCustomer.API.Contracts;
using CreateCustomer.API.Entities;

namespace CreateCustomer.API.Repositories
{
    public class SalesTerritoryRepository : GenericRepository<SalesTerritory>, ISalesTerritoryRepository
    {
        public SalesTerritoryRepository() : this(new CustomerContext())
        {

        }
        public SalesTerritoryRepository(CustomerContext context) : base(context)
        {

        }
    }
}
