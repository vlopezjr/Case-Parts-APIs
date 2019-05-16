using CreateCustomer.API.Contracts;
using CreateCustomer.API.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateCustomer.API.Repositories
{
    public class CCTransactionRepository : GenericRepository<CCTransaction>, ICCTransactionRepository
    {
        public CCTransactionRepository() : base(new CustomerContext())
        {

        }

        public CCTransactionRepository(CustomerContext context) : base(context)
        {

        }

        public async Task<List<CCTransaction>> GetCCTransactionsByOPAsync(int opKey, int limit)
        {
            return await _context.CCTransactions
                .Where(c => c.OPKey == opKey)
                .OrderByDescending(c => c.CreateDate)
                .Take(limit)
                .ToListAsync();
        }
    }
}
