using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateCustomer.API.Entities
{
    [Table("tciProcCycle")]
    public class StatementCycle : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ProcCycleKey")]
        public override int Key { get; set; }

        [Column("ProcCycleID")]
        public string Id { get; set; }

        [Column("CompanyID")]
        public string CompanyId { get; set; }
    }
}
