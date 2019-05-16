using CreateCustomer.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreateCustomer.API.Contracts
{
    public interface ICPSalesOrderRepository : IRepository<CPSalesOrder>
    {
        Task<List<CPSalesOrder>> GetCPSalesOrdersAsync(string op, int limit);
        Task<CPSalesOrder> GetCPSalesOrderAsync(int op);
        Task<List<CPSalesOrder>> GetOnHoldCPSalesOrdersAsync();
        Task<List<CPSalesOrder>> GetOnHoldCPSalesOrdersAsync(string collector);
        Task<CPSalesOrder> GetCreditCardByOP(int opKey);
        Task<int> GetOPByPOAsync(int po);
    }
}
