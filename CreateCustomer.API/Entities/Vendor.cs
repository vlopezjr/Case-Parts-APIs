using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateCustomer.API.Entities
{
    [Table("tapVendor")]
    public class Vendor : BaseEntity
    {
        [Key]
        [Column("VendKey")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Key { get; set; }
        public short ACHTranCode { get; set; }
        public short AllowPOBackOrder { get; set; }
        public Nullable<int> BuyerKey { get; set; }
        public Nullable<int> CashAcctKey { get; set; }
        public string CompanyID { get; set; }
        public short CreditCardVendor { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public short CreateType { get; set; }
        public string CreateUserID { get; set; }
        public decimal CreditLimit { get; set; }
        public Nullable<int> CurrExchSchdKey { get; set; }
        public Nullable<int> DfltItemKey { get; set; }
        public Nullable<int> DfltPurchAcctKey { get; set; }
        public int DfltPurchAddrKey { get; set; }
        public int DfltRemitToAddrKey { get; set; }
        public short HoldPmt { get; set; }
        public Nullable<int> ImportLogKey { get; set; }
        public Nullable<int> MatchToleranceKey { get; set; }
        public Nullable<int> PmtTermsKey { get; set; }
        public Nullable<int> POFormKey { get; set; }
        public int PrimaryAddrKey { get; set; }
        public Nullable<int> PrimaryCntctKey { get; set; }
        public string RcvgCmnt { get; set; }
        public short RequirePOIssue { get; set; }
        public short RequireRMA { get; set; }
        public Nullable<decimal> RetntRate { get; set; }
        public short SeparateChk { get; set; }
        public short Status { get; set; }
        public string TaxPayerID { get; set; }
        public int UpdateCounter { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateUserID { get; set; }
        public string UserFld1 { get; set; }
        public string UserFld2 { get; set; }
        public string UserFld3 { get; set; }
        public string UserFld4 { get; set; }
        public string V1099Control { get; set; }
        public string V1099Box { get; set; }
        public Nullable<short> V1099FATCA { get; set; }
        public Nullable<short> V1099Form { get; set; }
        public short V1099Type { get; set; }
        public int VendClassKey { get; set; }
        public string VendDBA { get; set; }
        public string VendID { get; set; }
        public string VendName { get; set; }
        public Nullable<int> VendPmtMethKey { get; set; }
        public string VendRefNo { get; set; }
        public string VouchCmnt { get; set; }

        [ForeignKey("DfltPurchAddrKey")]
        public virtual VendorAddress VendorAddress { get; set; }
    }
}
