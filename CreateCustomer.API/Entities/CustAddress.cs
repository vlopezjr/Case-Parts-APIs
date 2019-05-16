using CreateCustomer.API.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateCustomer.API.Entities
{
    [Table("tarCustAddr")]
    public class CustAddress : BaseEntity
    {
        [Key]
        [Column("AddrKey")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Key { get; set; }
        public short AllowInvtSubst { get; set; }
        public short BackOrdPrice { get; set; }
        public short BOLReqd { get; set; }
        public string CarrierAcctNo { get; set; }
        public short CarrierBillMeth { get; set; }
        public short CloseSOLineOnFirstShip { get; set; }
        public short CloseSOOnFirstShip { get; set; }
        public int? CommPlanKey { get; set; }
        public DateTime? CreateDate { get; set; }
        public short CreateType { get; set; }
        public string CreateUserID { get; set; }
        public int? CreditCardKey { get; set; }
        public int? CurrExchSchdKey { get; set; }
        public string CurrID { get; set; }
        public string CustAddrID { get; set; }
        public int? CustPriceGroupKey { get; set; }
        public int? DfltCntctKey { get; set; }
        public int? FOBKey { get; set; }
        public short FreightMethod { get; set; }
        public int? ImportLogKey { get; set; }
        public int? InvcFormKey { get; set; }
        public string InvcMsg { get; set; }
        public short InvoiceReqd { get; set; }
        public int LanguageID { get; set; }
        public short PackListContentsReqd { get; set; }
        public int? PackListFormKey { get; set; }
        public short PackListReqd { get; set; }
        public int? PmtTermsKey { get; set; }
        public decimal PriceAdj { get; set; }
        public short PriceBase { get; set; }
        public short PrintOrderAck { get; set; }
        public short RequireSOAck { get; set; }
        public int? SalesTerritoryKey { get; set; }
        public short ShipComplete { get; set; }
        public short ShipDays { get; set; }
        public int? ShipLabelFormKey { get; set; }
        public short ShipLabelsReqd { get; set; }
        public int? ShipMethKey { get; set; }
        public int? ShipZoneKey { get; set; }
        public int? SOAckFormKey { get; set; }
        public short SOAckMeth { get; set; }
        public int? SperKey { get; set; }
        public int? STaxSchdKey { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateUserID { get; set; }
        public int? WhseKey { get; set; }
        public short UsePromoPrice { get; set; }
        public int CustKey { get; set; }
        [NotMapped]
        public CustAddrType Type { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Address Address { get; set; }
        public virtual List<TaxExemptionAcuity> TaxExemptionsAcuity { get; set; }
        [ForeignKey("STaxSchdKey")]
        public virtual TaxSchedule TaxSchedule { get; set; }
    }
}
