using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateCustomer.API.Entities
{
    [Table("tarNationalAcct")]
    public class NationalAccount : BaseEntity
    {
        [Key]
        [Column("NationalAcctKey")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Key { get; set; }
        public string CompanyID { get; set; }
        public decimal CreditLimit { get; set; }
        public short CreditLimitUsed { get; set; }
        public string Description { get; set; }
        public short Hold { get; set; }
        public string NationalAcctID { get; set; }

    }
}
