using CreateCustomer.API.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CreateCustomer.UnitTest
{
    [TestClass]
    public class CustHoldTests
    {
        [TestMethod]
        public void GetLateCustomersOnHold()
        {
            var repo = new CustHoldRepository();
            var customers = repo.GetLateCustomersByCollector("PamS");


            Assert.IsTrue(customers.Count > 0);
        }

        [TestMethod]
        public void GetOverCustomersOnHold()
        {
            var repo = new CustHoldRepository();
            var customers = repo.GetOverCustomersByCollector("PamS");


            Assert.IsTrue(customers.Count > 0);
        }

        [TestMethod]
        public void GetCreditCustomersOnHold()
        {
            var repo = new CustHoldRepository();
            var customers = repo.GetCreditCustomersByCollector("PamS");


            Assert.IsTrue(customers.Count > 0);
        }

        [TestMethod]
        public void GetCustomerLetters45Query()
        {
            var repo = new CustHoldRepository();
            var customers45 = repo.GetCustomersOver45Days();

            Assert.IsTrue(customers45.Count > 0);
        }


        [TestMethod]
        public void GetCustomerLetters60Query()
        {
            var repo = new CustHoldRepository();
            var customers60 = repo.GetCustomersOver60Days();

            Assert.IsTrue(customers60.Count > 0);
        }

        [TestMethod]
        public void GetCustomerLetters90Query()
        {
            var repo = new CustHoldRepository();
            var customers90 = repo.GetCustomersOver90Days();

            Assert.IsTrue(customers90.Count > 0);
        }
    }
}
