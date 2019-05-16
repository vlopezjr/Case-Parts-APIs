using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateCustomer.API.Entities
{
    [Table("tarCustClass")]
    public class CustClass : BaseEntity
    {
        [Key]
        [Column("CustClassKey")]
        public override int Key { get; set; }
        public short AllowInvtSubst { get; set; }
        public short AllowCustRefund { get; set; }
        public short AllowWriteOff { get; set; }
        public short BillingType { get; set; }
        [Column("CompanyID")]
        public string CompanyId { get; set; }
        public decimal CreditLimit { get; set; }
        public short CreditLimitAgeCat { get; set; }
        public string CurrID { get; set; }
        public int? DfltSalesAcctKey { get; set; }
        public decimal FinChgFlatAmt { get; set; }
        public decimal? FinChgPct { get; set; }
        public int? InvcFormKey { get; set; }
        [Column("CustClassId")]
        public string Id { get; set; }
        public int LanguageID { get; set; }
        public int? FOBKey { get; set; }
        [Column("CustClassName")]
        public string Name { get; set; }
        public short RequireSOAck { get; set; }
        public int? PmtTermsKey { get; set; }
        public short ShipComplete { get; set; }
        public int? ShipLabelFormKey { get; set; }
        public int ShipMethKey { get; set; }
        public int? SOAckFormKey { get; set; }
        public int? SperKey { get; set; }
        public int? StmtCycleKey { get; set; }
        public int? StmtFormKey { get; set; }
        public decimal? TradeDiscPct { get; set; }        
        public short ReqPO { get; set; }
        public short PrintDunnMsg { get; set; }
        public decimal? RetntPct { get; set; }

    }
}
