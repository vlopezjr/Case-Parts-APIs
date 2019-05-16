using CreateCustomer.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreateCustomer.API.Contracts
{
    public interface IMemoRemarkRepository : IRepository<MemoRemark>
    {
        Task<List<MemoRemark>> GetMemoRemarksByTypeAndOwnerKey(int ownerKey, string remarkType);
    }
}
