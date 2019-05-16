using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateCustomer.API.Entities
{
    [Table("tarSalesTerritory")]
    public class SalesTerritory : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("SalesTerritoryKey")]
        public override int Key { get; set; }
        public string CompanyID { get; set; }
        public string SalesTerritoryID { get; set; }
    }
}
