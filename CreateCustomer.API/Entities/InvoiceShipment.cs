using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateCustomer.API.Entities
{
    [Table("tsoInvoiceShipment")]
    public class InvoiceShipment : BaseEntity
    {
        [Key]
        [Column("ShipKey")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Key { get; set; }
        public int InvcKey { get; set; }

        [ForeignKey("InvcKey")]
        public virtual Invoice Invoice { get; set; }
    }
}
