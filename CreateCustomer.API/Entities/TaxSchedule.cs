using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateCustomer.API.Entities
{
    [Table("tciSTaxSchedule")]
    public class TaxSchedule : BaseEntity
    {
        [Key]
        [Column("STaxSchdKey")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Key { get; set; }
        public string STaxSchdDesc { get; set; }
        public string STaxSchdID { get; set; }
        public int UpdateCounter { get; set; }
        public virtual List<TaxCode> TaxCodes { get; set; }

        [NotMapped]
        public decimal Rate
        {
            get
            {
                if(rate == 0)
                {
                    rate = 0;

                    foreach (var taxCode in TaxCodes)
                    {
                        rate += taxCode.TaxSubjClass.Rate;
                    }

                    return rate = rate * 100;
                }
                else
                {
                    return rate;
                }

            }
        }
        [NotMapped]
        private decimal rate;
    }
}
