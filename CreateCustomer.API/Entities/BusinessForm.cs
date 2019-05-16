using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateCustomer.API.Entities
{
    [Table("tciBusinessForm")]
    public class BusinessForm : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("BusinessFormKey")]
        public override int Key { get; set; }
        public string BusinessFormID { get; set; }
        public short BusinessFormType { get; set; }
        public string CompanyID { get; set; }
        public string Description { get; set; }
    }
}
