using CreateCustomer.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreateCustomer.API.Contracts
{
    public interface ICustPaymentRepository : IRepository<CustPayment>
    {
        Task<List<CustPayment>> GetCustPaymentWithTranNo(string tranNo, int limit);
    }
}
