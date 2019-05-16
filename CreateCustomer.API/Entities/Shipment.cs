using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateCustomer.API.Entities
{
    [Table("tsoShipment")]
    public class Shipment : BaseEntity
    {
        [Key]
        [Column("ShipKey")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Key { get; set; }
        public string TranNo { get; set; }

        public virtual List<ShipLine> ShipLines { get; set; }
    }
}
