using CreateCustomer.API.Contracts;
using CreateCustomer.API.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Threading.Tasks;

namespace CreateCustomer.API.Repositories
{
    public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository() : base(new CustomerContext())
        {

        }

        public InvoiceRepository(CustomerContext context) : base(context)
        {

        }

        public async Task<List<Invoice>> GetInvoicesByTranNo(string tranNo, int limit)
        {
            return await _context.Invoices
                            .Include("Customer")
                            .Where(invc => invc.TranNo.Contains(tranNo) && invc.CompanyID == "CPC")
                            .OrderByDescending(invc => invc.TranDate)
                            .Take(limit)
                            .ToListAsync();
        }

        public async Task<List<Invoice>> GetInvoicesByPONumber(string poNumber, int limit)
        {
            return await _context.Invoices
                            .Include("Customer")
                            .Where(invc => invc.CustPONo.Contains(poNumber) && invc.CompanyID == "CPC")
                            .OrderByDescending(invc => invc.TranDate)
                            .Take(limit)
                            .ToListAsync();
        }

        public async Task<List<Invoice>> GetInvoicesByTranAmt(decimal tranAmt, int limit)
        {
            return await _context.Invoices
                            .Include("Customer")
                            .Where(invc => invc.TranAmt.ToString().Contains(tranAmt.ToString()) && invc.CompanyID == "CPC")
                            .OrderByDescending(invc => invc.TranDate)
                            .Take(limit)
                            .ToListAsync();
        }
    }
}

