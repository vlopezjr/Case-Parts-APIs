using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateCustomer.API.Entities
{
    [Table("tcpGroupMember")]
    public class GroupMember : BaseEntity
    {

        public int UserKey { get; set; }
        public int GroupKey { get; set; }

        [NotMapped]
        public override int Key { get; set; }
    }
}
