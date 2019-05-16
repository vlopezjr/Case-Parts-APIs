using System;
using System.Threading.Tasks;
using CreateCustomer.API.DomainServices;
using CreateCustomer.API.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CreateCustomer.UnitTest
{
    [TestClass]
    public class ARResearchTests
    {
        [TestMethod]
        public void TestCustomerToCustPmtRelationship()
        {
            var custRepo = new CustomerRepository();
            var customer = custRepo.GetById(29301);

            Assert.IsTrue(customer.CustPayment.Count > 0);
        }

        [TestMethod]
        public void TestCustPmtToCustomerRelationship()
        {
            var service = new ARResearchService();
            var payments = service.GetCustPaymentsByTranNoAsync("022253").Result;

            Assert.IsTrue(payments.Count > 0);
            Assert.IsNotNull(payments[0].Customer);
        }

        [TestMethod]
        public void TestCustomerToInvoiceRelationship()
        {
            var custRepo = new CustomerRepository();
            var customer = custRepo.GetById(29301);

            Assert.IsTrue(customer.Invoices.Count > 0);
        }

        [TestMethod]
        public void TestInvoiceToCustomerRelationship()
        {
            var service = new ARResearchService();
            var invoices = service.GetInvoicesByTranNoAsync("0000010085").Result;

            Assert.IsTrue(invoices.Count > 0);
            Assert.IsNotNull(invoices[0].Customer);
        }

        [TestMethod]
        public void TestInvoiceRepositoryMethods()
        {
            var service = new ARResearchService();
            var tranNoInvoices = service.GetInvoicesByTranNoAsync("0000010085").Result;
            var poNoInvoices = service.GetInvoicesByPONumberAsync("DONALDS").Result;
            var amtInvoices = service.GetInvoicesByTranAmtAsync(100).Result;

            Assert.IsTrue(tranNoInvoices.Count > 0);
            Assert.IsTrue(poNoInvoices.Count > 0);
            Assert.IsTrue(amtInvoices.Count > 0);
        }

        [TestMethod]
        public void TestCPSalesOrderRepository()
        {
            var repo = new CPSalesOrderRepository();
            var orders = repo.GetCPSalesOrdersAsync("", 25).Result;

            Assert.IsNotNull(orders.Count > 2);
        }

        [TestMethod]
        public void TestSalesOrderRepository()
        {
            var repo = new SalesOrderRepository();
            var orders = repo.GetSalesOrdersAsync("00000", 25).Result;

            Assert.IsNotNull(orders.Count > 2);
        }

        [TestMethod]
        public void TestShipmentRepository()
        {
            var repo = new ShipmentRepository();
            var shipments = repo.GetShipmentsByTranNoAsync("00038", 25).Result;

            Assert.IsNotNull(shipments.Count > 2);
        }

        [TestMethod]
        public void TestARBatchStoredProc()
        {
            var repo = new BatchRepository();
            var batches = repo.GetARBatchesByBatchID("ARCR-0050090").Result;

            Assert.IsNotNull(batches.Count > 1);
        }


        [TestMethod]
        public void TestGetAPBatches()
        {
            var repo = new BatchRepository();
            var batches = repo.GetAPBatchesAsync("").Result;

            Assert.IsNotNull(batches.Count > 1);
        }


        [TestMethod]
        public void TestGetPOBatches()
        {
            var repo = new BatchRepository();
            var batches = repo.GetPOBatchesAsync("").Result;

            Assert.IsNotNull(batches.Count > 1);
        }

        [TestMethod]
        public void TestARServiceGetCreditCardByOP()
        {
            var repo = new ARResearchService();
            var cc = repo.GetCreditCardByOP(4216430).Result;

            Assert.IsNotNull(cc);
            Assert.IsNotNull(cc.CrCardNoDecrypted);
        }

        [TestMethod]
        public void GetShipmentBatchesMPK()
        {
            var service = new ARResearchService();
            var batches = service.GetShipmentBatchesAsync(23).Result;


            Assert.IsTrue(batches.Count > 0);
        }

        [TestMethod]
        public void GetShipmentCheckMPK()
        {
            var service = new ARResearchService();
            var batches = service.GetShipmentBatchesAsync(23).Result;

            var shipmentChecks = service.GetShipmentCheckAsync(batches[1].CreateDate, 23, batches[1].TypeKey, batches[1].PackStation).Result;


            Assert.IsTrue(shipmentChecks.Count > 0);
        }

        [TestMethod]
        public void GetMemoRemarksByOwnerKeyAndType()
        {
            var service = new ARResearchService();
            var remark = service.GetMemoRemarksByTypeAndOwnerKeyAsync(40343, "Cust.AR.Coll").Result;

            Assert.IsTrue(remark != null);
        }
    }
}
