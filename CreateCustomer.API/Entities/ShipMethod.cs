using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateCustomer.API.Entities
{
    [Table("tciShipMethod")]
    public class ShipMethod : BaseEntity
    {
        [Key]
        [Column("ShipMethKey")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Key { get; set; }
        public string ShipMethDesc { get; set; }
        public string ShipMethID { get; set; }
        [Column("STaxClassKey")]
        public int TaxClassKey { get; set; }
    }
}
