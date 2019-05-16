using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateCustomer.API.Entities
{
    [Table("tciSTaxSubjClassDt")]
    public class TaxSubjClass : BaseEntity
    {
        [Key]
        [Column("STaxCodeKey")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Key { get; set; }

        public decimal Rate { get; set; }



    }
}
