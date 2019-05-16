using CreateCustomer.API.Contracts;
using CreateCustomer.API.Entities;
using CreateCustomer.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateCustomer.API.DomainServices
{
    public class SalesOrderService
    {
        private readonly CustomerContext context;

        private readonly ICPSalesOrderRepository cpSORepository;

        public SalesOrderService()
        {
            context = new CustomerContext();
            cpSORepository = new CPSalesOrderRepository(context);
        }

        public async Task<List<CPSalesOrder>> GetOnHoldOrdersAsync()
        {
            return await cpSORepository.GetOnHoldCPSalesOrdersAsync();
        }

        public async Task<List<CPSalesOrder>> GetOnHoldOrdersAsync(string collector)
        {
            return await cpSORepository.GetOnHoldCPSalesOrdersAsync(collector);
        }

        public async Task<int> GetStatusByOPAsync(int op)
        {
            var order = await cpSORepository.GetCPSalesOrderAsync(op);
            return order == null ? 0 : order.StatusCode;
        }

        public async Task<int> GetOPByPOAsync(int po)
        {
            return await cpSORepository.GetOPByPOAsync(po);
        }
    }
}
