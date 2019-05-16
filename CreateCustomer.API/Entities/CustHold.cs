using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateCustomer.API.Entities
{
    [Table("tcpCustHold")]
    public class CustHold : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("CustKey")]
        public override int Key { get; set; }
        public int HoldStatusKey { get; set; }
        public string CustID { get; set; }
        public string CustName { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactPhoneExt { get; set; }
        public decimal Balance90 { get; set; }
        public decimal Balance60 { get; set; }
        public decimal Balance45 { get; set; }
        public decimal TotalBalance { get; set; }
        public decimal TotalLate { get; set; }
        public decimal CreditLimit { get; set; }
        public string PmtTerms { get; set; }
        public string Collector { get; set; }
        public DateTime OnHoldDate { get; set; }
        public short Letter45 { get; set; }
        public short Letter60 { get; set; }
        public short Letter90 { get; set; }

        [NotMapped]
        public User User { get; set; }


        [NotMapped]
        public decimal Over
        {
            get { return TotalBalance - CreditLimit; }
        }

        public virtual Customer Customer { get; set; }
        [ForeignKey("HoldStatusKey")]
        public virtual HoldStatus HoldStatus { get; set; }
    }

    [Table("tcpHoldStatus")]
    public class HoldStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HoldStatusKey { get; set; }
        public string HoldStatusID { get; set; }
        public string HoldStatusDescr { get; set; }
        public string HoldStatusType { get; set; }
        public short Hold { get; set; }
    }
}
