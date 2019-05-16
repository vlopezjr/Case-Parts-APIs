using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateCustomer.API.Entities
{
    [Table("tcpCCTransaction")]
    public class CCTransaction : BaseEntity
    {
        [Key]
        [Column("TranKey")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Key { get; set; }
        public int OPKey { get; set; }
        public DateTime CreateDate { get; set; }
        public string UserID { get; set; }
        public string PNREF { get; set; }
        public decimal Amount { get; set; }
        public string TranType { get; set; }
        public string Response { get; set; }
    }
}
