using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateCustomer.API.Entities
{
    [Table("tcpSTaxExempt")]
    public class TaxExemptionCPC : BaseEntity
    {
        [Key, Column(Order = 0)]
        public int CustKey { get; set; }
        [Key, Column(Order = 1)]
        public string State { get; set; }
        public string ExemptNo { get; set; }

        [ForeignKey("CustKey")]
        public virtual Customer Customer { get; set; }

        [NotMapped]
        public override int Key { get; set; }
    }
}
