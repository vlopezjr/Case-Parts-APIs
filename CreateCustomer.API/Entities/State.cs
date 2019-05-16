using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateCustomer.API.Entities
{
    [Table("tsmState")]
    public class State : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string StateID { get; set; }
        public string CountryID { get; set; }
        public string StateName { get; set; }
        [ForeignKey("CountryID")]
        public virtual Country Country { get; set; }
        [NotMapped]
        public override int Key { get; set; }
    }
}
