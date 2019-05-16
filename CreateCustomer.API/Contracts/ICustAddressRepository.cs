using CreateCustomer.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateCustomer.API.Contracts
{
    public interface ICustAddressRepository : IRepository<CustAddress>, IAsyncRepository<CustAddress>
    {
        Tuple<int, string, string> GetCATaxScheduleIdByCounty(string county);

        Tuple<int, string, string> GetCATaxScheduleIdByCity(string city);

        Tuple<int, string, string> GetWATaxScheduleIdByLocationCode(string locationCode);

        Tuple<int, string, string> GetTaxScheduleIdByZip(string zip);
    }
}
