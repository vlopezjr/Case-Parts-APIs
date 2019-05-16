namespace CreateCustomer.API.Entities
{
    public class ShipmentFreightBalance
    {
        public int ShipKey { get; set; }
        public decimal FreightAmt { get; set; }
        public int? WhseKey { get; set; }
        public decimal SalesAmt { get; set; }
        public decimal STaxAmt { get; set; }
        public short? DueDayOrMonth { get; set; }
        public string UserFld1 { get; set; }
    }
}
