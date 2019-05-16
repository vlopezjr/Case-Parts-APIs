using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateCustomer.API.Entities
{
    [Table("tciMemo")]
    public class MemoRemark : BaseEntity
    {
        [Key]
        [Column("MemoKey")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Key { get; set; }
        public string Addressee { get; set; }
        public string CompanyID { get; set; }
        public DateTime EffectiveDate { get; set; }
        public short EntityType { get; set; }
        public string MemoID { get; set; }
        public int MemoOwnerKey { get; set; }
        public string MemoText { get; set; }
        public string Sender { get; set; }
        public string Subject { get; set; }
    }
}
