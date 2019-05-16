using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateCustomer.API.Entities
{
    [Table("tapVendAddr")]
    public class VendorAddress : BaseEntity
    {
        [Key]
        [Column("AddrKey")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Key { get; set; }
        public string BankAcctName { get; set; }
        public string BankAcctNo { get; set; }
        public string BankName { get; set; }
        public string BankRoutingTrnstNo { get; set; }
        public DateTime? CreateDate { get; set; }
        public short CreateType { get; set; }
        public string CreateUserID { get; set; }
        public Nullable<int> CurrExchSchdKey { get; set; }
        public string CurrID { get; set; }
        public Nullable<int> DfltCntctKey { get; set; }
        public Nullable<int> FOBKey { get; set; }
        public Nullable<int> ImportLogKey { get; set; }
        public int LanguageID { get; set; }
        public Nullable<int> ShipMethKey { get; set; }
        public Nullable<int> ShipZoneKey { get; set; }
        public Nullable<int> STaxSchdKey { get; set; }
        public int UpdateCounter { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateUserID { get; set; }
        public string VendAddrID { get; set; }
        public int VendKey { get; set; }
    }
}
