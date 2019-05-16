using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateCustomer.API.Entities
{
    [Table("tarInvoice")]
    public class Invoice : BaseEntity
    {
        [Key]
        [Column("InvcKey")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Key { get; set; }
        public string TranCmnt { get; set; }
        public DateTime TranDate { get; set; }
        public string CustPONo { get; set; }
        public decimal TranAmt { get; set; }
        public string TranID { get; set; }
        public int CustKey { get; set; }
        public string TranNo { get; set; }
        public string CompanyID { get; set; }

        [ForeignKey("CustKey")]
        public virtual Customer Customer { get; set; }
    }
}
