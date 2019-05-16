using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateCustomer.API.Entities
{
    [Table("tcpSOLine")]
    public class CPSOLine : BaseEntity
    {
        [Key]
        [Column("OPLineKey")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Key { get; set; }
        public int OPKey { get; set; }
        public int LineNo { get; set; }
        public Nullable<int> SOLineKey { get; set; }
        public Nullable<int> ItemType { get; set; }
        public Nullable<int> ItemKey { get; set; }
        public string ItemID { get; set; }
        public string Descr { get; set; }
        public Nullable<int> VendKey { get; set; }
        public Nullable<int> MakeKey { get; set; }
        public string CabModelNbr { get; set; }
        public string CabSerialNbr { get; set; }
        public string ModModelNbr { get; set; }
        public string ModSerialNbr { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public Nullable<int> Qty { get; set; }
        public Nullable<int> QtyReturned { get; set; }
        public decimal BackNegotiatedPrice { get; set; }
        public string CatPage { get; set; }
        public string ComboMakeModelSerial { get; set; }
        public int CustType { get; set; }
        public decimal DealerPrice { get; set; }
        public decimal Depth { get; set; }
        public int DoorHeight { get; set; }
        public int DoorWidth { get; set; }
        public decimal EffectivePrice { get; set; }
        public decimal ExtendedPrice { get; set; }
        public int Feet { get; set; }
        public int FinishID { get; set; }
        public string FinishText { get; set; }
        public int FrameID { get; set; }
        public string FrameText { get; set; }
        public string GMPartNbr { get; set; }
        public decimal Height { get; set; }
        public int Inches { get; set; }
        public bool IsCGMPN { get; set; }
        public bool IsDart { get; set; }
        public bool IsLA { get; set; }
        public bool IsMagnetic { get; set; }
        public bool IsSinglePass { get; set; }
        public bool IsSL { get; set; }
        public bool IsTaxable { get; set; }
        public bool IsThreeSided { get; set; }
        public int LineKey { get; set; }
        public decimal ListPrice { get; set; }
        public int MaterialID { get; set; }
        public int MorphBTOKey { get; set; }
        public decimal NegotiatedPrice { get; set; }
        public decimal OhmsPerFoot { get; set; }
        public int Options { get; set; }
        public int ResearchStatus { get; set; }
        public int StatusCode { get; set; }
        public int TotalInches { get; set; }
        public int Voltage { get; set; }
        public decimal WholeSalePrice { get; set; }
        public string WhseBinID { get; set; }
        public decimal Width { get; set; }


        [ForeignKey("OPKey")]
        public virtual CPSalesOrder CPSalesOrder { get; set; }

        [ForeignKey("VendKey")]
        public virtual Vendor Vendor { get; set; }
    }
}
