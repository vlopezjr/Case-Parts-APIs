using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateCustomer.API.Entities
{
    [Table("tarCustPmt")]
    public class CustPayment : BaseEntity
    {
        [Key]
        [Column("CustPmtKey")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Key { get; set; }
        public int CustKey { get; set; }
        public string TranNo { get; set; }
        public decimal TranAmt { get; set; }
        public DateTime TranDate { get; set; }
        public string CompanyID { get; set; }

        [ForeignKey("CustKey")]
        public virtual Customer Customer { get; set; }
    }
}
