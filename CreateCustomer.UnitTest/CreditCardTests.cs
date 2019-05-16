using System;
using CreateCustomer.API.DomainServices;
using CreateCustomer.API.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CreateCustomer.UnitTest
{
    [TestClass]
    public class CreditCardTests
    {
        //CREDIT CARDS
        [TestMethod]
        public void LoadCreditCardWithStoredProc()
        {
            var service = new CustomerService();
            var cc = service.LoadCreditCardByKey(1006);

            Assert.IsTrue(cc != null);
            Assert.IsTrue(!string.IsNullOrEmpty(cc.CrCardNoDecrypted));
        }

        [TestMethod]
        public void UpdateCreditCard()
        {
            var service = new CustomerService();
            var cc = service.LoadCreditCardByKey(1006);

            //-1 means preferred
            cc.Preferred = 0;
            service.UpdateCreditCard(cc);
            var cachedPrefValue = cc.Preferred;

            cc = new CreditCard();
            cc = service.LoadCreditCardByKey(1006);

            Assert.IsTrue(cc.Preferred == cachedPrefValue);
        }

        [TestMethod]
        public void UpdateCreditCardStatus()
        {
            var service = new CustomerService();
            var cc = service.LoadCreditCardByKey(1006);

            service.UpdateCreditCardStatusToInActive(cc.CCKey); //sets status to 2

            cc = new CreditCard();
            cc = service.LoadCreditCardByKey(1006);

            Assert.IsTrue(cc.Status == 2);
        }

        [TestMethod]
        public void LoadCreditCardKeysByCustKey()
        {
            var service = new CustomerService();
            var listOfCCKeys = service.LoadCreditCardsByCustKey(29301);


            Assert.IsTrue(listOfCCKeys.Count > 0);
        }

        [TestMethod]
        public void LoadCreditCardsByCustKey()
        {
            var service = new CustomerService();
            var listOfCreditCards = service.LoadCreditCardsByCustKey(29301);

            Assert.IsTrue(listOfCreditCards.Count > 0);
        }


        [TestMethod]
        public void LoadCreditCardTypes()
        {
            var service = new LookUpService();
            var allcardTypes = service.GetAllCreditCardTypes();

            Assert.IsTrue(allcardTypes.Count > 0);
        }

        [TestMethod]
        public void TestGetCreditCardByOP()
        {
            var repo = new ARResearchService();
            var cc = repo.GetCreditCardByOP(4216430).Result;

            Assert.IsNotNull(cc);
            Assert.IsNotNull(cc.CrCardNoDecrypted);
        }
    }
}
