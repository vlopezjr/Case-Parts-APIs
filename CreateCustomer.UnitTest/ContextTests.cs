using System;
using System.Linq;
using CreateCustomer.API.Entities;
using CreateCustomer.API.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CreateCustomer.UnitTest
{
    [TestClass]
    public class ContextTests
    {
        [TestMethod]
        public void LoadAllCustomerClasses()
        {
            var repo = new CustClassRepository();
            var custClasses = repo.GetAll();

            Assert.IsNotNull(custClasses);
            Assert.IsTrue(custClasses.Count>0);
        }

        [TestMethod]
        public void LoadAllCPCCustomerClasses()
        {
            var repo = new CustClassRepository();
            var custClasses = repo.GetAllByCompanyId();

            Assert.IsNotNull(custClasses);
            Assert.IsTrue(custClasses.Count > 0);
            Assert.AreEqual(custClasses.Count, 3);
        }

        [TestMethod]
        public void LoadEndUserCustomerClass()
        {
            var repo = new CustClassRepository();
            var custClass = repo.GetById(38);

            Assert.IsNotNull(custClass);
            Assert.AreEqual(custClass.Key, 38);           
        }

        [TestMethod]
        public void LoadDealerCustomerClass()
        {
            var repo = new CustClassRepository();
            var custClass = repo.GetById(40);

            Assert.IsNotNull(custClass);
            Assert.AreEqual(custClass.Key, 40);
        }

        [TestMethod]
        public void LoadWholesaleCustomerClass()
        {
            var repo = new CustClassRepository();
            var custClass = repo.GetById(44);

            Assert.IsNotNull(custClass);
            Assert.AreEqual(custClass.Key, 44);
        }

        [TestMethod]
        public void LoadAllPaymentTerms()
        {
            var repo = new PaymentTermsRepository();
            var terms = repo.GetAll();

            Assert.IsNotNull(terms);
            Assert.IsTrue(terms.Count > 0);
        }

        [TestMethod]
        public void LoadAllCPCPaymentTerms()
        {
            var repo = new PaymentTermsRepository();
            var terms = repo.GetAllByCompanyId();

            Assert.IsNotNull(terms);
            Assert.IsTrue(terms.Count > 0);
            Assert.AreEqual(terms.Count, 28);
        }

        [TestMethod]
        public void LoadOneContact()
        {
            var repo = new ContactRepository();
            var contact = repo.GetById(201685);

            Assert.IsNotNull(contact);
            Assert.AreEqual(contact.Key, 201685);
            //Assert.AreEqual(contact.Name, "Lydia Arciniaga");
            Assert.AreEqual(contact.Title, "ACCOUNTING");
        }

        [TestMethod]
        public void LoadOneContactWithSharedPropertyOn()
        {
            var repo = new ContactRepository();
            var contact = repo.GetById(201688);

            Assert.IsNotNull(contact);
            Assert.AreEqual(contact.Key, 201688);            
            Assert.AreEqual(contact.Title, "ACCOUNTING");
            Assert.IsNotNull(contact.ParentKey);
            Assert.IsTrue(contact.Shared);
        }

        [TestMethod]
        public void LoadOneAddress()
        {
            var repo = new AddressRepository();
            var address = repo.GetById(4497525);

            Assert.IsNotNull(address);
            Assert.AreEqual(address.Key, 4497525);
            Assert.AreEqual(address.Line1, "8819 Whittier Blvd");
            Assert.AreEqual(address.Line2, "Suite 105");
            Assert.AreEqual(address.City.Trim(), "Pico Rivera");
            Assert.AreEqual(address.Country, "USA");
            Assert.AreEqual(address.Zip, "90660");
            Assert.AreEqual(address.Residential, 0);
        }

        [TestMethod]
        public void LoadOneCustomer()
        {
            var repo = new CustomerRepository();
            var customer = repo.GetById(58025);

            Assert.IsNotNull(customer);
            Assert.AreEqual(customer.Key, 58025);
            Assert.AreEqual(customer.Id.Trim(), "RLARC90660");
          
        }

        [TestMethod]
        public void LoadOneCustomerWithContacts()
        {
            var repo = new CustomerRepository();
            var customer = repo.GetWithDependenciesById("REFRI98682");

            Assert.IsNotNull(customer);
            Assert.AreEqual(customer.Key, 65479);
            Assert.AreEqual(customer.Id.Trim(), "REFRI98682");

        }

        [TestMethod]
        public void LoadCustomerWithBranches()
        {
            var repo = new CustomerRepository();
            var customer = repo.GetWithDependenciesById("RLARC90660");

            Assert.IsNotNull(customer);
            Assert.AreEqual(customer.Key, 58025);
            Assert.AreEqual(customer.Id.Trim(), "RLARC90660");
            //Assert.AreEqual(customer.Branches.Count, 3);
            Assert.IsTrue(customer.IsHQ);
            Assert.IsFalse(customer.IsBranch);
        }

        [TestMethod]
        public void LoadCustomerBranchesCheckForParent()
        {
            var repo = new CustomerRepository();
            var customer = repo.GetWithDependenciesById("MCDON90201");

            Assert.IsNotNull(customer);
            Assert.AreEqual(customer.Key, 58027);
            //Assert.AreEqual(customer.Id.Trim(), "MCDON90201");
            Assert.AreEqual(customer.Branches.Count, 0);
            Assert.AreEqual(customer.Parents.Count, 1);
           // Assert.AreEqual(customer.Parents[0].Id.Trim(), "RLARC90660");
            Assert.IsFalse(customer.IsHQ);
            Assert.IsTrue(customer.IsBranch);
        }

        

        [TestMethod]
        public void LoadContactsByParentContactKey()
        {
            var repo = new ContactRepository();
            var contacts = repo.GetAllByParentContactKey(201685);

            Assert.IsNotNull(contacts);
            Assert.AreEqual(contacts.Count, 4);
          

        }

        [TestMethod]
        public void LoadContactsByCustKey()
        {
            var repo = new ContactRepository();
            var contacts = repo.GetAllByCustKey(6293);

            Assert.IsNotNull(contacts);
            Assert.IsTrue(contacts.Count > 0);


        }

        [TestMethod]
        public void LoadAndUpdateContactsByParentContactKey()
        {
            CustomerContext context = new CustomerContext();

            var repo = new ContactRepository(context);
            var contacts = repo.GetAllByParentContactKey(201685);

            Assert.IsNotNull(contacts);
            Assert.AreEqual(contacts.Count, 4);

            contacts.ForEach(x => x.MobilePhone = "6265551212");

            foreach (Contact c in contacts)
                repo.Update(c);
                        
            context.SaveChanges();
                      
            contacts = repo.GetAllByParentContactKey(201685);

            foreach (Contact c in contacts)
                Assert.AreEqual(c.MobilePhone, "6265551212");

        }

        //[TestMethod]
        //public void LoadCustomerUpdateDTAndReload()
        //{
        //    CustomerContext context = new CustomerContext();
        //    var repo = new CustomerRepository(context);

        //    var customer = repo.GetWithDependenciesByKey(58025);

        //    var invoice = customer.Contacts.Find(c => c.IsPrimary).Invoice;

        //    repo.UpdateHQPrimaryDocTransmittal(58025, 501, !invoice);

        //    var customer2 = repo.GetWithDependenciesByKey(58025);

        //    foreach(Customer c in customer2.Branches)
        //    {
        //        Assert.AreEqual(c.Contacts.Find(i => i.IsPrimary).Invoice, !invoice);
        //    }     

        //}

        [TestMethod]
        public void SearchByCustIdShouldNotFindAMatch()
        {
            var repo = new CustomerRepository();
            var results = repo.SearchCustIdByString("julie91722");
            Assert.IsNull(results);
        }

        [TestMethod]
        public void SearchByCustIdShouldFindAMatch()
        {
            var repo = new CustomerRepository();
            var results = repo.SearchCustIdByString("abcor10001");
            var id = results.Item1;
            var name = results.Item2;
            Assert.AreEqual(id.ToLower(), "abcor10001-1");
        }

        [TestMethod]
        public void GetCATaxScheduleByCountyShouldReturnValidKey()
        {
            var repo = new CustAddressRepository();
            var key = repo.GetCATaxScheduleIdByCounty("Los Angeles Co");            
            Assert.AreEqual(key.Item1, 36);
            Assert.AreEqual(key.Item2, "Los Angeles Co");
            Assert.AreEqual(key.Item3, "0.095000");
        }

        [TestMethod]
        public void GetCATaxScheduleByCountyShouldReturnInvalidKey()
        {
            var repo = new CustAddressRepository();
            var key = repo.GetCATaxScheduleIdByCounty("Los Angeles");
            Assert.AreEqual(key.Item1, 0);
            Assert.AreEqual(key.Item2, "");
            Assert.AreEqual(key.Item3, "");
        }

        [TestMethod]
        public void GetCATaxScheduleByCityShouldReturnValidKey()
        {
            var repo = new CustAddressRepository();
            var key = repo.GetCATaxScheduleIdByCity("South Gate");
            Assert.AreEqual(key.Item1, 711);
            Assert.AreEqual(key.Item2, "South Gate");
            Assert.AreEqual(key.Item3, "0.102500");
        }

        [TestMethod]
        public void GetCATaxScheduleByCityShouldReturnInvalidKey()
        {
            var repo = new CustAddressRepository();
            var key = repo.GetCATaxScheduleIdByCity("Norwalk");
            Assert.AreEqual(key.Item1, 0);
            Assert.AreEqual(key.Item2, "");
            Assert.AreEqual(key.Item3, "");
        }

        [TestMethod]
        public void GetWATaxScheduleByLocationCodeShouldReturnValidKey()
        {
            var repo = new CustAddressRepository();
            var key = repo.GetWATaxScheduleIdByLocationCode("1715");
            Assert.AreEqual(key.Item1, 293);
            Assert.AreEqual(key.Item2.Trim(), "1715 + WA Base");
            Assert.AreEqual(key.Item3, "0.100000");
        }

        [TestMethod]
        public void GetMOTaxScheduleByLocationZipCodeShouldReturnValidKey()
        {
            var repo = new CustAddressRepository();
            var key = repo.GetTaxScheduleIdByZip("63045");
            Assert.AreEqual(key.Item1, 170);
            Assert.AreEqual(key.Item2.Trim(), "Missouri");
            Assert.AreEqual(key.Item3, "0.088630");
        }

        
    }
}
