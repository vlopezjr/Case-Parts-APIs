using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CreateCustomer.API.Entities
{
    [Table("tarCustDocTrnsmit")]
    public class DocTransmittal : BaseEntity
    {
        [Key]        
        [Column("CustKey", Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Key { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TranType { get; set; }

        public short EMail { get; set; }

        public short EMailFormat { get; set; }

        public short Fax { get; set; }

        public short HardCopy { get; set; }

        public short IncludeCC { get; set; }

        [ForeignKey("Key")]
        public virtual Customer Customer { get; set; }
    }
}
