using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateCustomer.API.Entities
{
    [Table("tcpUser")]
    public class User : BaseEntity
    {
        [Key]
        [Column("UserKey")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Key { get; set; }
        public string UserID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public short? IsLoggedOn { get; set; }
        public DateTime UpdateTime { get; set; }
        public string BranchID { get; set; }
        public short IsActive { get; set; }
        public virtual List<Group> Groups { get; set; }
    }
}
