using CreateCustomer.API.Entities;
using System.Collections.Generic;

namespace CreateCustomer.API.Contracts
{
    public interface ICustHoldRepository : IRepository<CustHold>
    {
        List<CustHold> GetLateCustomersByCollector(string collector);
        List<CustHold> GetOverCustomersByCollector(string collector);
        List<CustHold> GetCreditCustomersByCollector(string collector);
        List<CustHold> GetCustomersOver45Days();
        List<CustHold> GetCustomersOver60Days();
        List<CustHold> GetCustomersOver90Days();
    }
}
