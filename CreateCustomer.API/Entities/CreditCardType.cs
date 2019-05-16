using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateCustomer.API.Entities
{
    [Table("tcpCreditCardType")]
    public class CreditCardType : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("CrCardTypeKey")]
        public override int Key { get; set; }
        public string CrCardTypeName { get; set; }
        public string CrCardTypeMask { get; set; }
    }
}
