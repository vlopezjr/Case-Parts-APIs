using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateCustomer.API.Entities
{
    [Table("tsmCountry")]
    public class Country : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CountryID { get; set; }
        public string CurrID { get; set; }
        public string Name { get; set; }
        public virtual List<State> States { get; set; }

        [NotMapped]
        public override int Key { get; set; }

    }
}
