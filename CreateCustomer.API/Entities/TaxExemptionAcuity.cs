using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateCustomer.API.Entities
{
    [Table("tarCustSTaxExmpt")]
    public class TaxExemptionAcuity : BaseEntity
    {
        [NotMapped]
        public override int Key { get; set; }

        public string ExmptNo { get; set; }

        [Key, Column(Order = 1)]
        public int STaxCodeKey { get; set; }
        [Key, Column(Order = 0)]
        public int AddrKey { get; set; }


        [ForeignKey("AddrKey")]
        public virtual CustAddress CustAddress { get; set; }
        [ForeignKey("STaxCodeKey")]
        public virtual TaxCode TaxCode { get; set; }
    }
}
