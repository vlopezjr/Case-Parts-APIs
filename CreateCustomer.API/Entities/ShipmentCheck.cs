namespace CreateCustomer.API.Entities
{
    public class ShipmentCheck
    {
        public int ShipKey { get; set; }
        public int OPNbr { get; set; }
        public string Shipment { get; set; }
        public decimal OrderAmt { get; set; }
        public string SONbr { get; set; }
        public decimal FreightAmt { get; set; }
        public string ShipMethID { get; set; }
        public byte ShipComplete { get; set; }
        public byte FreeFreight { get; set; }
        public byte ReducedFreight { get; set; }
        public byte BillDifferentRate { get; set; }
        public byte PartsNoCharge { get; set; }
        public byte InboundFreight { get; set; }
        public byte Deposit { get; set; }
        public byte TaxError { get; set; }
        public decimal STaxAmt { get; set; }
        public int ShipAddrSTaxSchdKey { get; set; }
        public string ShipAddrSTaxExemptNo { get; set; }
        public int BillMethKey { get; set; }
        public string ShipTrackNo { get; set; }
        public int Backorders { get; set; }
        public string UPSAcct { get; set; }
        public string ShipToCountryID { get; set; }
        public int HasTrueCompressor { get; set; }
        public string PaymentTers { get; set; }
        public int WhseKey { get; set; }
        public string CreateUserID { get; set; }
    }
}
