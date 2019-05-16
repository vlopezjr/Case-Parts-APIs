using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateCustomer.API.Entities
{
    [Table("tcpGroup")]
    public class Group : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("GroupKey")]
        public override int Key { get; set; }

        public string GroupID { get; set; }
        public string Caption { get; set; }
        public string Comments { get; set; }
        public virtual List<User> Users { get; set; }
    }
}
