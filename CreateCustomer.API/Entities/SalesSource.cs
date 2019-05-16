using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateCustomer.API.Entities
{
    [Table("tsoSalesSource")]
    public class SalesSource : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("SalesSourceKey")]
        public override int Key { get; set; }
        public string CompanyID { get; set; }
        public string Description { get; set; }
        public string SalesSourceID { get; set; }
    }
}
