using CreateCustomer.API.Contracts;
using CreateCustomer.API.Entities;
using CreateCustomer.API.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreateCustomer.API.DomainServices
{
    public class ARResearchService
    {
        private CustomerContext context = new CustomerContext();

        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ISalesOrderRepository _salesOrderRepository;
        private readonly ICPSalesOrderRepository _cpSalesOrderRepository;
        private readonly IShipmentRepository _shipmentRepository;
        private readonly ICustPaymentRepository _custPaymentRepository;
        private readonly ICCTransactionRepository _ccTransactionRepository;
        private readonly CreditCardRepository creditCardRepository;
        private readonly ShipmentBatchRepository shipmentBatchRepository;
        private readonly IMemoRemarkRepository memoRemarkRepository;

        public ARResearchService()
        {
            _invoiceRepository = new InvoiceRepository(context);
            _salesOrderRepository = new SalesOrderRepository(context);
            _cpSalesOrderRepository = new CPSalesOrderRepository(context);
            _shipmentRepository = new ShipmentRepository(context);
            _custPaymentRepository = new CustPaymentRepository(context);
            _ccTransactionRepository = new CCTransactionRepository(context);
            creditCardRepository = new CreditCardRepository(context);
            shipmentBatchRepository = new ShipmentBatchRepository(context);
            memoRemarkRepository = new MemoRemarkRepository(context);
        }


        public async Task<List<CustPayment>> GetCustPaymentsByTranNoAsync(string tranNo, int limit = 25)
        {
            return await _custPaymentRepository.GetCustPaymentWithTranNo(tranNo, limit);
        }

        public async Task<List<Invoice>> GetInvoicesByTranNoAsync(string tranNo, int limit = 25)
        {
            return await _invoiceRepository.GetInvoicesByTranNo(tranNo, limit);
        }

        public async Task<List<Invoice>> GetInvoicesByPONumberAsync(string poNumber, int limit = 25)
        {
            return await _invoiceRepository.GetInvoicesByPONumber(poNumber, limit);
        }

        public async Task<List<Invoice>> GetInvoicesByTranAmtAsync(decimal tranAmt, int limit = 25)
        {
            return await _invoiceRepository.GetInvoicesByTranAmt(tranAmt, limit);
        }

        public async Task<List<SalesOrder>> GetSalesOrdersByTranNoAsync(string tranNo, int limit = 25)
        {
            return await _salesOrderRepository.GetSalesOrdersAsync(tranNo, limit);
        }

        public async Task<List<CPSalesOrder>> GetCPSalesOrdersByOPAsync(string opKey, int limit = 25)
        {
            return await _cpSalesOrderRepository.GetCPSalesOrdersAsync(opKey, limit);
        }

        public async Task<List<Shipment>> GetShipmentsByTranNoAsync(string tranNo, int limit = 25)
        {
            return await _shipmentRepository.GetShipmentsByTranNoAsync(tranNo, limit);
        }

        public async Task<List<CCTransaction>> GetCCTransactionsByOPAsync(int opKey, int limit = 25)
        {
            return await _ccTransactionRepository.GetCCTransactionsByOPAsync(opKey, limit);
        }

        public async Task<CreditCard> GetCreditCardByOP(int opKey)
        {
            var order = await _cpSalesOrderRepository.GetCreditCardByOP(opKey);
            return order == null ? null : order.CreditCard;
        }

        public async Task<List<ShipmentBatch>> GetShipmentBatchesAsync(int whseKey)
        {
            return await shipmentBatchRepository.GetShipmentBatchesAsync(whseKey);
        }

        public async Task<List<ShipmentCheck>> GetShipmentCheckAsync(string createDate, int whseKey, int typeKey, string packStation)
        {
            return await shipmentBatchRepository.GetShipmentCheckAsync(createDate, whseKey, typeKey, packStation);
        }

        public async Task<List<MemoRemark>> GetMemoRemarksByTypeAndOwnerKeyAsync(int ownerKey, string remarkType)
        {
            return await memoRemarkRepository.GetMemoRemarksByTypeAndOwnerKey(ownerKey, remarkType);
        }

        public async Task BalanceFreightAsync(int typeKey, string createDate, int whseKey, string packstation)
        {
            await shipmentBatchRepository.BalanceFreight(typeKey, createDate, whseKey, packstation);
        }
    }
}
