using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateCustomer.API.Entities
{
    [Table("tsoSalesOrder")]
    public class SalesOrder : BaseEntity
    {
        [Key]
        [Column("SOKey")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Key { get; set; }
        public int CustKey { get; set; }
        public string TranNo { get; set; }

        public virtual List<SOLine> SOLines { get; set; }
        [ForeignKey("CustKey")]
        public virtual Customer Customer { get; set; }
    }
}
