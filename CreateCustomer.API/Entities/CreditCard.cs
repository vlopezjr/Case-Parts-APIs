using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreateCustomer.API.Entities
{
    public class CreditCard 
    {
        public int CCKey { get; set; }
        public string CrCardExp { get; set; }
        public string CrCardNo { get; set; }
        public string CardHolderName { get; set; }
        public int? CrCardTypeKey { get; set; }
        public string CrCardTypeName { get; set; }
        public string CrCardStreetNbrZip { get; set; }
        public string CrCardZipCode { get; set; }
        public short? Status { get; set; }
        public string CodedCCNo { get; set; }
        public short? Preferred { get; set; }
        public string CrCardNoDecrypted { get; set; }
    }
}
