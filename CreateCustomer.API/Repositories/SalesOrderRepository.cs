using CreateCustomer.API.Contracts;
using CreateCustomer.API.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CreateCustomer.API.Repositories
{
    public class SalesOrderRepository : GenericRepository<SalesOrder>, ISalesOrderRepository
    {
        public SalesOrderRepository() : base(new CustomerContext()) { }

        public SalesOrderRepository(CustomerContext context) : base(context) { }

        public async Task<List<SalesOrder>> GetSalesOrdersAsync(string tranNo, int limit)
        {
            return await _context.SalesOrders
                .Where(so => so.TranNo.Contains(tranNo) && 
                             so.SOLines.Any(soLine => soLine.ShipLines.Any(sl => sl.InvoiceShipment.Invoice.CompanyID == "CPC")))
                .OrderByDescending(so => so.TranNo)
                .Take(limit)
                .ToListAsync();
        }
    }
}
