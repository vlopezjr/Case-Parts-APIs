using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateCustomer.API.Entities
{
    [Table("tcpBranch")]
    public class Branch : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("WhseKey")]
        public override int Key { get; set; }
        public string BranchID { get; set; }
        public int? CustKey { get; set; }
        public string BranchName { get; set; }
        public int ShipMethKey { get; set; }
        public int SalesTerritoryKey { get; set; }
        public int SalesAcctKey { get; set; }
        public int WireShelfVendKey { get; set; }
        public string PhoneNumber { get; set; }
        public string Number800 { get; set; }
        public string FaxNumber { get; set; }
    }
}
