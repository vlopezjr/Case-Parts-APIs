using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateCustomer.API.Entities
{
    [Table("tciSTaxCode")]
    public class TaxCode : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("STaxCodeKey")]
        public override int Key { get; set; }
        public string Description { get; set; }
        public short PrintOnInvc { get; set; }
        public short RoundMeth { get; set; }
        public string STaxCodeID { get; set; }
        public short Taxable { get; set; }
        public int UpdateCounter { get; set; }
        public virtual List<TaxSchedule> TaxSchedules { get; set; }
        [ForeignKey("Key")]
        public virtual TaxSubjClass TaxSubjClass { get; set; }
    }
}
