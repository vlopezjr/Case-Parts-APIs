using CreateCustomer.API.Contracts;
using CreateCustomer.API.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CreateCustomer.API.Repositories
{
    public class ShipmentRepository : GenericRepository<Shipment>, IShipmentRepository
    {
        public ShipmentRepository(CustomerContext context) : base(context) { }
        public ShipmentRepository() : base(new CustomerContext()) { }

        public async Task<List<Shipment>> GetShipmentsByTranNoAsync(string tranNo, int limit)
        {
            return await _context.Shipments
                .Where(shp => shp.TranNo.Contains(tranNo) &&
                              shp.ShipLines.Any(sl => sl.InvoiceShipment.Invoice.CompanyID == "CPC"))
                .OrderByDescending(shp => shp.TranNo)
                .Take(limit)
                .ToListAsync();
        }
    }
}
