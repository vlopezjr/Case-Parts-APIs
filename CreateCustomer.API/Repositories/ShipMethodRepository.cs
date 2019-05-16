using CreateCustomer.API.Contracts;
using CreateCustomer.API.Entities;

namespace CreateCustomer.API.Repositories
{
    public class ShipMethodRepository : GenericRepository<ShipMethod>, IShipMethodRepository
    {
        public ShipMethodRepository() : base(new CustomerContext()) { }
        public ShipMethodRepository(CustomerContext context) : base(context)
        {

        }
    }
}
