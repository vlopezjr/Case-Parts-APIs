using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateCustomer.API.Entities
{
    [Table("tarNationalAcctLevel")]
    public class NationalAccountLevel : BaseEntity
    {
        [Key]
        [Column("NationalAcctLevelKey")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Key { get; set; }
        public string Description { get; set; }
        public int NationalAcctKey { get; set; }
        public short NationalAcctLevel { get; set; }

        [ForeignKey("NationalAcctKey")]
        public virtual NationalAccount NationalAccount { get; set; }
    }
}
