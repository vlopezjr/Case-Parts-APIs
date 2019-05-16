using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateCustomer.API.Entities
{
    [Table("tsoSOLine")]
    public class SOLine : BaseEntity
    {
        [Key]
        [Column("SOLineKey")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Key { get; set; }
        public int SOKey { get; set; }

        [ForeignKey("SOKey")]
        public virtual SalesOrder SalesOrder { get; set; }

        public virtual List<ShipLine> ShipLines { get; set; }
    }
}
