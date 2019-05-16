namespace CreateCustomer.API.Entities
{
    public class Batch : BaseEntity
    {
        public string BatchCmnt { get; set; }
        public string TranNo { get; set; }
        public decimal? TranAmt { get; set; }
    }
}
