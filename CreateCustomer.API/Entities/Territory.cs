using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateCustomer.API.Entities
{
    [Table("tcpTerritory")]
    public class Territory : BaseEntity
    {
        [Key]
        [Column("BranchID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string BranchID { get; set; }
        public string CountryID { get; set; }
        public string StateID { get; set; }

        [NotMapped]
        public override int Key { get; set; }
    }
}
