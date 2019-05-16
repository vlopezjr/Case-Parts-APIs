using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateCustomer.API.Entities
{
    [Table("tciPaymentTerms")]
    public class PaymentTerms : BaseEntity
    {
        [Key]
        [Column("PmtTermsKey")]
        public override int Key { get; set; }

        [Column("PmtTermsId")]
        public string Id { get; set; }

        [Column("CompanyID")]
        public string CompanyId { get; set; }

        public short DiscDateOption { get; set; }
    }
}
