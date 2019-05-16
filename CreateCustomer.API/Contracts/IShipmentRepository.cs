using CreateCustomer.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreateCustomer.API.Contracts
{
    public interface IShipmentRepository : IRepository<Shipment> 
    {
        Task<List<Shipment>> GetShipmentsByTranNoAsync(string tranNo, int limit);
    }
}
