using CreateCustomer.API.Contracts;
using CreateCustomer.API.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CreateCustomer.API.Repositories
{
    public class CPSalesOrderRepository : GenericRepository<CPSalesOrder>, ICPSalesOrderRepository
    {
        public CPSalesOrderRepository() : base(new CustomerContext())
        {

        }

        public CPSalesOrderRepository(CustomerContext context) : base(context)
        {

        }

        public async Task<List<CPSalesOrder>> GetCPSalesOrdersAsync(string op, int limit)
        {
            return await _context.CPSalesOrders
                .Where(c => c.Key.ToString().Contains(op) &&
                            c.SalesOrder.SOLines.Any(line => line.ShipLines.Any(o => o.InvoiceShipment.Invoice.CompanyID == "CPC")))
                .OrderByDescending(c => c.Key)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<List<CPSalesOrder>> GetOnHoldCPSalesOrdersAsync()
        {
            return await _context.CPSalesOrders
                .Where(c => c.StatusCode == 5)
                .OrderByDescending(c => c.Key)
                .ToListAsync();
        }

        public async Task<List<CPSalesOrder>> GetOnHoldCPSalesOrdersAsync(string collector)
        {
            return await _context.CPSalesOrders
                .Where(c => c.StatusCode == 5 && c.Customer.UserFld1.Contains(collector))
                .OrderByDescending(c => c.Key)
                .ToListAsync();
        }

        public async Task<CPSalesOrder> GetCPSalesOrderAsync(int op)
        {
            return await _context.CPSalesOrders
                .FirstOrDefaultAsync(c => c.Key == op);
        }

        public async Task<CPSalesOrder> GetCreditCardByOP(int opKey)
        {
            var order = await _context.CPSalesOrders
                .FirstOrDefaultAsync(c => c.Key == opKey);

            if (order == null)
                return order;
            else
            {
                var sqlParameter = new SqlParameter("@creditCardKey", order.CCKey);

                order.CreditCard = await _context
                    .Database
                    .SqlQuery<CreditCard>("spcpcCreditCardLoad @creditCardKey", sqlParameter)
                    .FirstOrDefaultAsync();

                return order;
            }
        }

        public async Task<int> GetOPByPOAsync(int po)
        {
            return await _context
                .Database
                .SqlQuery<int>(String.Format(@"SELECT DISTINCT tcpSO.OPKey 
                                                FROM tpoPurchOrder
                                                INNER JOIN tpoPOLine ON tpoPOLine.POKey = tpoPurchOrder.POKey
                                                INNER JOIN tsoSOLine ON tpoPOLine.POLineKey = tsoSOLine.POLineKey
                                                INNER JOIN tsoSalesOrder ON tsoSOLine.SOKey = tsoSalesOrder.SOKey
                                                INNER JOIN tcpSO ON tsoSalesOrder.SOKey = tcpSO.SOKey
                                                WHERE tpoPurchOrder.tranno LIKE '%{0}'", po)).FirstOrDefaultAsync();
        }
    }
}
