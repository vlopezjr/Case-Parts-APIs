using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateCustomer.API.Entities
{
    [Table("tarCustStatus")]
    public class CustStatus : BaseEntity
    {
        [Key]        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("CustKey")]
        public override int Key { get; set; }
        public decimal AgeCat1Amt { get; set; }
        public decimal AgeCat2Amt { get; set; }
        public decimal AgeCat3Amt { get; set; }
        public decimal AgeCat4Amt { get; set; }
        public decimal AgeCurntAmt { get; set; }
        public decimal AgeFutureAmt { get; set; }
        public System.DateTime? AgingDate { get; set; }
        public decimal? AvgDaysPastDue { get; set; }
        public decimal? AvgDaysToPay { get; set; }
        public decimal AvgInvcAmt { get; set; }
        public decimal FinChgBal { get; set; }
        public decimal HighestBal { get; set; }
        public System.DateTime? HighestBalDate { get; set; }
        public int? HighestInvcKey { get; set; }
        public decimal LastStmtAmt { get; set; }
        public decimal LastStmtAmtNC { get; set; }
        public System.DateTime? LastStmtDate { get; set; }
        public short? NoInvcsInInvcAvg { get; set; }
        public short? NoInvcsInPmtAvg { get; set; }
        public decimal OnSalesOrdAmt { get; set; }
        public decimal RetntBal { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
