using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateCustomer.API.Entities
{
    [Table("tciContact")]
    public class Contact : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("CntctKey")]
        public override int Key { get; set; }
        public short CCCreditMemo { get; set; }
        public short CCCustStmnt { get; set; }
        public short CCDebitMemo { get; set; }
        public short CCEFTRemittance { get; set; }
        public short CCFinanceCharge { get; set; }
        public short CCInvoice { get; set; }
        public short CCPurchaseOrder { get; set; }
        public short CCRMA { get; set; }
        public short CCSalesOrder { get; set; }       
        public int CntctOwnerKey { get; set; }
        public short EMailFormat { get; set; } = 1;
        public short EntityType { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public string PhoneExt { get; set; }
        public string MobilePhone { get; set; }
        public string Fax { get; set; }
        public string FaxExt { get; set; }
        [Column("EmailAddr")]
        public string Email { get; set; }        
        public string FirstName { get; set; }        
        public string LastName { get; set; }        
        public short? Deleted { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateUserID { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateUserID { get; set; }
        public int UpdateCounter { get; set; }
        [Column("ParentCntctKey")]
        public int? ParentKey { get; set; }
        public bool Shared { get; set;}
        public string Module { get; set; }
        public virtual Customer Customer { get; set; }

        [NotMapped]
        public bool IsPrimary { get => Customer.PrimaryCntctKey == Key; }

        [NotMapped]
        public bool IsDeleted { get => Deleted == 0 ? false : true; }

        [NotMapped]
        public bool IsDirty { get; set; }

        [NotMapped]
        public bool IsNew { get => Key == 0 ? true : false; }

        [NotMapped]
	    public bool CreditMemo
	    {
		    get { return CCCreditMemo == 0 ? false : true;}
		    set { CCCreditMemo = value ? (short)1 : (short)0; }
	    }
        
        [NotMapped]
	    public bool Statement
	    {
		    get { return CCCustStmnt == 0 ? false : true;}
		    set { CCCustStmnt = value ? (short)1 : (short)0; }
	    }
        
        [NotMapped]
	    public bool Invoice
	    {
		    get { return CCInvoice == 0 ? false : true;}
		    set { CCInvoice = value ? (short)1 : (short)0; }
	    }

        [NotMapped]
        public bool DebitMemo
        {
            get { return CCDebitMemo == 0 ? false : true; }
            set { CCDebitMemo = value ? (short)1 : (short)0; }
        }
    }
}
