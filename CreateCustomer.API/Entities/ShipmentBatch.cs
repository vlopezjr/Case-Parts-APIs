using System;

namespace CreateCustomer.API.Entities
{
    public class ShipmentBatch : BaseEntity
    {
        public string Warehouse { get; set; }
        public string CreateDate { get; set; }
        public string TypeID { get; set; }
        public int TypeKey { get; set; }
        public string PackStation { get; set; }
        public int Shipments { get; set; }
        public decimal AmtShipped { get; set; }
    }
}
