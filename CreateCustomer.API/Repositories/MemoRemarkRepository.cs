using CreateCustomer.API.Contracts;
using CreateCustomer.API.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CreateCustomer.API.Repositories
{
    public class MemoRemarkRepository : GenericRepository<MemoRemark>, IMemoRemarkRepository
    {
        public MemoRemarkRepository() : base(new CustomerContext()) { }
        public MemoRemarkRepository(CustomerContext context) : base(context) { }

        public async Task<List<MemoRemark>> GetMemoRemarksByTypeAndOwnerKey(int ownerKey, string remarkType)
        {
            return await _context
                .MemoRemarks
                .Where(remark => remark.MemoOwnerKey == ownerKey && remark.Addressee == remarkType)
                .ToListAsync();
        }
    }
}
