using CreateCustomer.API.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CreateCustomer.UnitTest
{
    [TestClass]
    public class SalesOrdersTests
    {
        [TestMethod]
        public void TestCPSalesOrderToLines()
        {
            var repo = new CPSalesOrderRepository();
            var order = repo.GetCPSalesOrderAsync(4216434).Result;

            Assert.IsTrue(order.CPSOLines.Count > 2);
        }

        [TestMethod]
        public void GetShipMethodThroughOrderObject()
        {
            var repo = new CPSalesOrderRepository();
            var order = repo.GetCPSalesOrderAsync(4216434).Result;

            Assert.IsNotNull(order.ShipMethod);
        }

        [TestMethod]
        public void GetCustomerThroughOrderObject()
        {
            var repo = new CPSalesOrderRepository();
            var order = repo.GetCPSalesOrderAsync(4216434).Result;

            Assert.IsNotNull(order.Customer);
        }

        [TestMethod]
        public void GetOrdersOnHoldByCollector()
        {
            var repo = new CPSalesOrderRepository();
            var orders = repo.GetOnHoldCPSalesOrdersAsync("LoriR").Result;

            Assert.IsNotNull(orders.Count > 0);
        }

        [TestMethod]
        public void GetAllOrdersOnHold()
        {
            var repo = new CPSalesOrderRepository();
            var orders = repo.GetOnHoldCPSalesOrdersAsync().Result;

            Assert.IsNotNull(orders.Count > 0);
        }

        [TestMethod]
        public void GetWarehouseFromOrder()
        {
            var repo = new CPSalesOrderRepository();
            var order = repo.GetCPSalesOrderAsync(4216436).Result;

            Assert.IsNotNull(order.Warehouse);
        }

        [TestMethod]
        public void GetWarehousesThroughWarehouseRepo()
        {
            var repo = new BranchRepository();
            var order = repo.GetAll();

            Assert.IsNotNull(order);
            Assert.IsTrue(order.Count > 2);
        }

        [TestMethod]
        public void GetVendorFromCPSalesOrderItem()
        {
            var repo = new CPSalesOrderRepository();
            var order = repo.GetCPSalesOrderAsync(4216436).Result;

            Assert.IsNotNull(order.CPSOLines[0].Vendor);
        }

        [TestMethod]
        public void GetOPByPONumber()
        {
            var repo = new CPSalesOrderRepository();
            var opNumber = repo.GetOPByPOAsync(2111).Result;

            Assert.IsTrue(opNumber > 0);
        }
    }
}
