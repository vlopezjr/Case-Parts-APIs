using CreateCustomer.API.Contracts;
using CreateCustomer.API.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CreateCustomer.API.Repositories
{
    public class CustPaymentRepository : GenericRepository<CustPayment>, ICustPaymentRepository
    {
        public CustPaymentRepository() : base(new CustomerContext())
        {

        }

        public CustPaymentRepository(CustomerContext context) : base (context)
        {

        }

        public async Task<List<CustPayment>> GetCustPaymentWithTranNo(string tranNo, int limit)
        {
            return await _context.CustPayments
                                .Include("Customer")
                                .Where(c => c.TranNo.Contains(tranNo) && c.CompanyID == "CPC")
                                .Take(limit)
                                .ToListAsync();
        }
    }
}
