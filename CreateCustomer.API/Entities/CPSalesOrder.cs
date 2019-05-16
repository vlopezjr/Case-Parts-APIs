using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateCustomer.API.Entities
{
    [Table("tcpSO")]
    public class CPSalesOrder : BaseEntity
    {
        [Key]
        [Column("OPKey")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Key { get; set; }
        public int SOKey { get; set; }
        public int CustKey { get; set; }
        public int? CCKey { get; set; }
        public int StatusCode { get; set; }
        public string UserID { get; set; }
        public int WhseKey { get; set; }
        public int PmtTermsKey { get; set; }
        public string PurchOrd { get; set; }
        public string OrderedBy { get; set; }
        public int BillAddrKey { get; set; }
        public int ShipAddrKey { get; set; }
        public string CustName { get; set; }
        public string CustType { get; set; }
        public string CustXML { get; set; }
        public string BillAddrXML { get; set; }
        public string ShipAddrXML { get; set; }
        public int ShipMethKey { get; set; }
        public short? TaxStatusCode { get; set; }
        public short? IsDropShip { get; set; }
        public short? ReqPO { get; set; }
        public short? ShipComplete { get; set; }
        public short? PrintPickList { get; set; }
        public short? PricePackList { get; set; }
        public string Keywords { get; set; }
        public string Summary { get; set; }
        public string Info { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? TotalTax { get; set; }
        public int? Flags { get; set; }
        public int? ResearchStatus { get; set; }
        public int? BillMethKey { get; set; }
        public string UPSAcct { get; set; }
        public int? CntctKey { get; set; }
        public string ShipToName { get; set; }
        public string ShipToPhone { get; set; }
        public string ShipToNote { get; set; }
        public short? HasReceipt { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }


        [ForeignKey("SOKey")]
        public virtual SalesOrder SalesOrder { get; set; }

        [ForeignKey("CustKey")]
        public virtual Customer Customer { get; set; }

        [ForeignKey("ShipMethKey")]
        public virtual ShipMethod ShipMethod { get; set; }

        [ForeignKey("PmtTermsKey")]
        public virtual PaymentTerms PaymentTerms { get; set; }

        [ForeignKey("WhseKey")]
        public virtual Branch Warehouse { get; set; }

        public virtual List<CPSOLine> CPSOLines { get; set; }



        [NotMapped]
        public virtual CreditCard CreditCard { get; set; }

    }
}
