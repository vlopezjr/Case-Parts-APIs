using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateCustomer.API.Entities
{
    [Table("tsoShipLine")]
    public class ShipLine : BaseEntity
    {
        [Key]
        [Column("ShipLineKey")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Key { get; set; }
        public int SOLineKey { get; set; }
        public int ShipKey { get; set; }

        [ForeignKey("SOLineKey")]
        public virtual SOLine SOLine { get; set; }

        [ForeignKey("ShipKey")]
        public virtual InvoiceShipment InvoiceShipment { get; set; }

        [ForeignKey("ShipKey")]
        public virtual Shipment Shipment { get; set; }
    }
}
