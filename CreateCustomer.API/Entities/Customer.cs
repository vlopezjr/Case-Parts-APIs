using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateCustomer.API.Entities
{
    [Table("tarCustomer")]
    public class Customer : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("CustKey")]
        public override int Key { get; set; }
        public string AcctType { get; set; }

        public string ABANo { get; set; }

        public short AllowCustRefund { get; set; }

        public short AllowWriteOff { get; set; }

        public short BillingType { get; set; }

        public short BillToNationalAcctParent { get; set; }
        [Column("CustId")]
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public short ConsolidatedStatement { get; set; }

        public DateTime? CreateDate { get; set; }

        public short CreateType { get; set; }

        public string CreateUserID { get; set; }

        public decimal CreditLimit { get; set; }

        public short CreditLimitAgeCat { get; set; }

        public short CreditLimitUsed { get; set; }

        public string CRMCustID { get; set; }

        public int? CurrExchSchdKey { get; set; }

        public int CustClassKey { get; set; }

        [Column("CustName")]
        public string Name { get; set; }

        [Column("CustRefNo")]
        public string RefNo { get; set; }

        public DateTime? DateEstab { get; set; }

        public int DfltBillToAddrKey { get; set; }

        public int? DfltItemKey { get; set; }

        public decimal DfltMaxUpCharge { get; set; }

        public short DfltMaxUpChargeType { get; set; }

        public int? DfltSalesAcctKey { get; set; }

        public int? DfltSalesReturnAcctKey { get; set; }

        public int DfltShipToAddrKey { get; set; }

        public decimal FinChgFlatAmt { get; set; }

        public decimal? FinChgPct { get; set; }

        public short Hold { get; set; }

        public int? ImportLogKey { get; set; }

        public int? NationalAcctLevelKey { get; set; }

        public short PmtByNationalAcctParent { get; set; }

        public int PrimaryAddrKey { get; set; }

        public int? PrimaryCntctKey { get; set; }

        public short PrintDunnMsg { get; set; }
        public decimal? ReqCreditLimit { get; set; }
        public short ReqPO { get; set; }
        public decimal? RetntPct { get; set; }
        public int? SalesSourceKey { get; set; }
        public short ShipPriority { get; set; }
        public short Status { get; set; }
        public string StdIndusCodeID { get; set; }
        public int? StmtCycleKey { get; set; }
        public int? StmtFormKey { get; set; }
        public decimal? TradeDiscPct { get; set; }
        public int UpdateCounter { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateUserID { get; set; }
        public string UserFld1 { get; set; }
        public string UserFld2 { get; set; }
        public string UserFld3 { get; set; }
        public string UserFld4 { get; set; }
        public int? VendKey { get; set; }
        public virtual List<Contact> Contacts { get; set; }
        [ForeignKey("Key")]
        public virtual List<DocTransmittal> DocTransmittals { get; set; }
        public virtual List<Customer> Branches { get; set; }
        public virtual List<Customer> Parents { get; set; }
        [ForeignKey("PrimaryAddrKey")]
        public virtual Address PrimaryAddress { get; set; }

        [ForeignKey("DfltBillToAddrKey")]
        public virtual Address DefaultBillToAddress { get; set; }

        [ForeignKey("DfltShipToAddrKey")]
        public virtual Address DefaultShipToAddress { get; set; }

        [ForeignKey("PrimaryCntctKey")]
        public virtual Contact PrimaryContact { get; set; }

        public virtual CustStatus CustStatus { get; set; }
        public virtual CustHold CustHold { get; set; }

        public virtual List<CustAddress> CustAddresses { get; set; }

        [ForeignKey("NationalAcctLevelKey")]
        public virtual NationalAccountLevel NationalAccountLevel { get; set; }
        public virtual List<TaxExemptionCPC> TaxExemptionsCPC {get; set;}
        public virtual List<CustPayment> CustPayment { get; set; }
        public virtual List<Invoice> Invoices { get; set; }
        public virtual List<CPSalesOrder> Orders { get; set; }
        public virtual List<SalesOrder> AcuityOrders { get; set; }

        [NotMapped]
        public List<CreditCard> CreditCards { get; set; }

        [NotMapped]
        public DocTransmittal InvoicesTransmittal { get => DocTransmittals.Find(d => d.TranType == 501); }
        [NotMapped]
        public DocTransmittal StatementsTransmittal { get => DocTransmittals.Find(d => d.TranType == 502); }
        [NotMapped]
        public DocTransmittal CreditMemosTransmittal { get => DocTransmittals.Find(d => d.TranType == 522); }


        [NotMapped]
        public bool IsHQ
        {
            get
            {
                if (NationalAccountLevel == null)
                    return false;

                return NationalAccountLevel.NationalAcctLevel == 1;
            }
        }

        [NotMapped]
        public bool IsBranch
        {
            get
            {
                if (NationalAccountLevel == null)
                    return false;

                return NationalAccountLevel.NationalAcctLevel == 2;
            }
        }

        [NotMapped]
        public bool IsStandAlone
        {
            get
            {
                return NationalAccountLevel == null;
            }
        }
    }
}
