using CreateCustomer.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreateCustomer.API.Contracts
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
        Task<List<Invoice>> GetInvoicesByTranNo(string tranNo, int limit);
        Task<List<Invoice>> GetInvoicesByPONumber(string poNumber, int limit);
        Task<List<Invoice>> GetInvoicesByTranAmt(decimal tranAmt, int limit);
    }
}
