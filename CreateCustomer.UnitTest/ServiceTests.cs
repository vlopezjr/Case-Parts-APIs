using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CreateCustomer.API.DomainServices;
using CreateCustomer.API.Entities;
using CreateCustomer.API.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CreateCustomer.UnitTest
{
    [TestClass]
    public class ServiceTests
    {
        //[TestMethod]
        //public void AddSingleContact()
        //{
        //    var contact = new Contact()
        //    {
        //        CntctOwnerKey = 285407,
        //        EntityType = 501,
        //        Name = "Test Testing",
        //        Title = "Sales",
        //        MobilePhone = "6265945271",
        //        Phone = "6262325365",
        //        Email = "vlopezjr@yahoo.com",
        //        EMailFormat = 1,
        //        CreateDate = DateTime.Now,
        //        CreateUserID = "VictorL",
        //        UpdateDate = DateTime.Now,
        //        UpdateUserID = "VictorL"
        //    };

        //    var service = new CustomerService();
        //    service.AddContact(contact);
        //}

        //[TestMethod]
        //public void AddSingleAddress()
        //{
        //    var address = new Address()
        //    {
        //        Line1 = "282 E. Benbow St.",
        //        City = "Covina",
        //        State = "CA",
        //        Zip = "91722",
        //        Residential = 0,
        //        //TransactionOverride = 0,
        //        UpdateCounter = 0
        //    };

        //    var service = new CustomerService();
        //    service.AddAddress(address);
        //}

        //[TestMethod]
        //public void AddSingleCustomer()
        //{
        //    //var customer = new Customer()
        //    //{
        //    //    Id = 
        //    //};

        //    //var service = new CustomerService();
        //    //service.AddCustomer(customer);

        //    Assert.IsTrue(true);
        //}

        [TestMethod]
        public void LoadCustomerWithDependenciesById()
        {

            var service = new CustomerService();
            var customer = service.LoadCustomerWithDependenciesById("DOMSR91754");

            Assert.IsNotNull(customer);
            //Assert.AreEqual(customer.Key, 65479);
            Assert.AreEqual(customer.Id.Trim(), "DOMSR91754");
            Assert.IsTrue(customer.Contacts.Count > 0);
            Assert.IsTrue(customer.DocTransmittals.Count > 0);
            Assert.AreEqual(customer.PrimaryAddrKey, customer.PrimaryAddress.Key);
            Assert.IsNotNull(customer.NationalAccountLevel.NationalAccount);
        }

        [TestMethod]
        public void LoadCustomerWithDependenciesByKey()
        {

            var service = new CustomerService();
            var customer = service.LoadCustomerWithDependenciesByKey(65479);

            Assert.IsNotNull(customer);
            Assert.AreEqual(customer.Key, 65479);
            Assert.AreEqual(customer.Id.Trim(), "REFRI98682");
            Assert.IsTrue(customer.Contacts.Count > 0);
            Assert.IsTrue(customer.DocTransmittals.Count > 0);
            Assert.AreEqual(customer.PrimaryAddrKey, customer.PrimaryAddress.Key);
            //Assert.AreEqual(customer.PrimaryAddrKey, customer.PrimaryCustAddress.Key);
            //Assert.AreEqual(customer.CustAddresses.Count, 1);
        }

        [TestMethod]
        public void UpdateASingleContact()
        {

            var service = new CustomerService();
            var customer = service.LoadCustomerWithDependenciesByKey(65479);
            var contact = customer.Contacts[0];
            contact.Name = "Testing1 Testing1";
            contact.Deleted = 0;
            service.UpdateContact(contact);

            var updatedContact = service.LoadContact(contact.Key);

            Assert.IsNotNull(updatedContact);
            Assert.AreEqual(contact.Key, updatedContact.Key);
            Assert.AreEqual(contact.Name, updatedContact.Name);
            Assert.AreEqual(contact.Deleted, updatedContact.Deleted);
        }

        [TestMethod]
        public void UpdateMultipleContacts()
        {

            var service = new CustomerService();
            var customer = service.LoadCustomerWithDependenciesByKey(65479);
            customer.Contacts.ForEach(c => { c.Deleted = 1; c.UpdateDate = DateTime.Now; c.UpdateUserID = "VictorL"; });
            service.UpdateCustomer(customer);

            Assert.IsTrue(true);
            var contacts = service.LoadAllContactsByCustKey(65479);

            var deleted1 = customer.Contacts.FindAll(c => c.Deleted == 0);
            var deleted2 = contacts.FindAll(c => c.Deleted == 0);

            var updateName1 = customer.Contacts.FindAll(c => c.UpdateUserID == "VictorL");
            var updateName2 = contacts.FindAll(c => c.UpdateUserID == "VictorL");

            Assert.IsNotNull(contacts);
            Assert.AreEqual(customer.Contacts.Count, contacts.Count);
            Assert.AreEqual(deleted1.Count, deleted2.Count);
            Assert.AreEqual(updateName1.Count, updateName2.Count);
        }

        [TestMethod]
        public void UpdateCustomerDT()
        {
            var service = new CustomerService();
            var customer = service.LoadCustomerWithDependenciesByKey(65479);
            customer.InvoicesTransmittal.IncludeCC = 1;
            customer.CreditMemosTransmittal.IncludeCC = 1;
            customer.StatementsTransmittal.IncludeCC = 1;
            service.UpdateCustomer(customer);


            var updatedCustomer = service.LoadCustomer(65479);

            Assert.AreEqual(updatedCustomer.InvoicesTransmittal.EMail, 1);
            Assert.AreEqual(updatedCustomer.CreditMemosTransmittal.IncludeCC, 1);
            Assert.AreEqual(updatedCustomer.StatementsTransmittal.IncludeCC, 1);
        }

        [TestMethod]
        public void LoadAndUpdateContactsByParentContactKey()
        {
            CustomerService service = new CustomerService();
            var contacts = service.LoadAllContactsByParentContactKey(201685);

            Assert.IsNotNull(contacts);
            Assert.AreEqual(contacts.Count, 4);

            contacts.ForEach(x => x.MobilePhone = "6265551111");

            service.UpdateContacts(contacts);

            contacts = service.LoadAllContactsByParentContactKey(201685);

            foreach (Contact c in contacts)
                Assert.AreEqual(c.MobilePhone, "6265551111");

        }

        //[TestMethod]
        //public void LoadCustomerUpdateDTAndReload()
        //{
        //    var service = new CustomerService();       

        //    var customer = service.LoadCustomerWithDependenciesByKey(58025);

        //    var invoice = !customer.Contacts.Find(c => c.IsPrimary).Invoice;

        //    Console.WriteLine("HQ: " + invoice);

        //    service.UpdateHQPrimaryDocTransmittal(58025, 501, invoice);

        //    var customer2 = service.LoadCustomerWithDependenciesByKey(58029);           
        //    var branchInvoice = customer2.Contacts.Find(i => i.IsPrimary).Invoice;

        //    Console.WriteLine("Branch: " + branchInvoice);
        //    Assert.AreEqual(branchInvoice, invoice);


        //}

        //[TestMethod]
        //public void LoadCustomerAndSwapPrimaryContact()
        //{
        //    // 201685 - Lydia
        //    // 285472 - Shared

        //    var service = new CustomerService();
        //    var customer = service.LoadCustomerWithDependenciesByKey(58025);

        //    var primaryContact = customer.Contacts.Find(c => c.IsPrimary);
        //    var newPrimaryContact = customer.Contacts.Find(c => c.Key == 201685);

        //    var primaryName = primaryContact.Name;
        //    var newPrimaryName = newPrimaryContact.Name;

        //    customer.PrimaryCntctKey = newPrimaryContact.Key;
        //    customer.PrimaryCustAddress.DfltCntctKey = newPrimaryContact.Key;

        //    newPrimaryContact.CCInvoice = customer.InvoicesTransmittal.EMail;
        //    newPrimaryContact.CCCreditMemo = customer.CreditMemosTransmittal.EMail;
        //    newPrimaryContact.CCCustStmnt = customer.StatementsTransmittal.EMail;
        //    newPrimaryContact.ParentKey = newPrimaryContact.Key;

        //    primaryContact.CCInvoice = 0;
        //    primaryContact.CCCreditMemo = 0;
        //    primaryContact.CCCustStmnt = 0;


        //    foreach(Customer branch in customer.Branches)
        //    {
        //        var branchName = branch.Id;
        //        var branchPrimary = branch.Contacts.Find(c => c.IsPrimary);
        //        var branchNewPrimaryContact = branch.Contacts.Find(c => c.ParentKey == newPrimaryContact.Key);

        //        var branchPrimaryName = branchPrimary.Name;
        //        var branchNewPrimaryName = branchNewPrimaryContact.Name;

        //        branch.PrimaryCntctKey = branchNewPrimaryContact.Key;
        //        branch.PrimaryCustAddress.DfltCntctKey = branchNewPrimaryContact.Key;                

        //        branchNewPrimaryContact.CCInvoice = branch.InvoicesTransmittal.EMail;
        //        branchNewPrimaryContact.CCCreditMemo = branch.CreditMemosTransmittal.EMail;
        //        branchNewPrimaryContact.CCCustStmnt = branch.StatementsTransmittal.EMail;
        //        branchNewPrimaryContact.ParentKey = newPrimaryContact.Key;

        //        branchPrimary.CCInvoice = 0;
        //        branchPrimary.CCCreditMemo = 0;
        //        branchPrimary.CCCustStmnt = 0;
        //    }


        //    service.UpdateExplicitlyWithDependencies(customer);
        //}

        [TestMethod]
        public void GetCATaxScheduleByCountyShouldReturnValidKey()
        {
            var service = new CustomerService();
            var key = service.GetCATaxScheduleByCounty("Los Angeles Co");
            Assert.AreEqual(key.Item1, 36);
            Assert.AreEqual(key.Item2, "Los Angeles Co");
            Assert.AreEqual(key.Item3, "0.095000");
        }

        [TestMethod]
        public void GetCATaxScheduleByCountyShouldReturnInvalidKey()
        {
            var service = new CustomerService();
            var key = service.GetCATaxScheduleByCounty("Los Angeles");
            Assert.AreEqual(key.Item1, 0);
            Assert.AreEqual(key.Item2, "");
            Assert.AreEqual(key.Item3, "");
        }

        [TestMethod]
        public void GetCATaxScheduleByCityShouldReturnValidKey()
        {
            var service = new CustomerService();
            var key = service.GetCATaxScheduleByCity("South Gate");
            Assert.AreEqual(key.Item1, 711);
            Assert.AreEqual(key.Item2, "South Gate");
            Assert.AreEqual(key.Item3, "0.102500");
        }

        [TestMethod]
        public void GetCATaxScheduleByCityShouldReturnInvalidKey()
        {
            var service = new CustomerService();
            var key = service.GetCATaxScheduleByCity("Norwalk");
            Assert.AreEqual(key.Item1, 0);
            Assert.AreEqual(key.Item2, "");
            Assert.AreEqual(key.Item3, "");
        }

        [TestMethod]
        public void GetWATaxScheduleByLocationCodeShouldReturnValidKey()
        {
            var service = new CustomerService();
            var key = service.GetWATaxScheduleByLocationCode("1715");
            Assert.AreEqual(key.Item1, 293);
            Assert.AreEqual(key.Item2.Trim(), "1715 + WA Base");
            Assert.AreEqual(key.Item3, "0.100000");
        }

        [TestMethod]
        public void GetMOTaxScheduleByZipCodeShouldReturnValidKey()
        {
            var service = new CustomerService();
            var key = service.GetTaxScheduleByZip("63045");
            Assert.AreEqual(key.Item1, 170);
            Assert.AreEqual(key.Item2.Trim(), "Missouri");
            Assert.AreEqual(key.Item3, "0.088630");
        }


        [TestMethod]
        public void AddCustomerWithDependencies()
        {
            var custId = "VMLOP91900-2";
            var custService = new CustomerService();
            var lookupService = new LookUpService();


            var custclasses = lookupService.GetCustClasses();
            var dealerCustClass = custclasses.Where(c => c.Key == 40).First();

            var primaryAddress = GetHydratedAddress("877 Monterey Pass Road");
            var primaryCustAddress = GetHydratedCustAddress(dealerCustClass, API.Enums.CustAddrType.Primary);

            var billtoAddress = GetHydratedAddress("875 Monterey Pass Road");
            var billtoCustAddress = GetHydratedCustAddress(dealerCustClass, API.Enums.CustAddrType.BillTo);

            var shiptoAddress = GetHydratedAddress("876 Monterey Pass Road");
            var shiptoCustAddress = GetHydratedCustAddress(dealerCustClass, API.Enums.CustAddrType.ShipTo);

            var primaryContact = new Contact()
            {
                Name = "Victor Lopez",
                FirstName = "Victor",
                LastName = "Lopez",
                Title = "Accounting",
                Email = "vlopezjr@yahoo.com",
                MobilePhone = "6262325365",
                Phone = "6262325365",
                PhoneExt = "",
                Fax = "",
                FaxExt = "",
                CCCreditMemo = 0,
                CCCustStmnt = 0,
                CCDebitMemo = 0,
                CCEFTRemittance = 0,
                CCFinanceCharge = 0,
                CCInvoice = 0,
                CCPurchaseOrder = 0,
                CCRMA = 0,
                CCSalesOrder = 0,
                Deleted = 0,
                EMailFormat = 3,
                EntityType = 501,
                UpdateCounter = 1
            };

            var newCustomer = new Customer();
            newCustomer.PrimaryAddress = primaryAddress;
            newCustomer.DefaultBillToAddress = billtoAddress;
            newCustomer.DefaultShipToAddress = shiptoAddress;
            newCustomer.PrimaryContact = primaryContact;
            //newCustomer.Contacts = new List<Contact>();
            //newCustomer.Contacts.Add(primaryContact);  


            newCustomer.Id = custId;
            newCustomer.AllowCustRefund = dealerCustClass.AllowCustRefund;
            newCustomer.AllowWriteOff = dealerCustClass.AllowWriteOff;
            newCustomer.BillingType = dealerCustClass.BillingType;
            newCustomer.BillToNationalAcctParent = 0;
            newCustomer.CompanyId = "CPC";
            newCustomer.ConsolidatedStatement = 0;
            newCustomer.CreateDate = DateTime.Now;
            newCustomer.CreateUserID = Environment.UserName;
            newCustomer.CreateType = 1;
            newCustomer.CreditLimit = dealerCustClass.CreditLimit;
            newCustomer.CreditLimitAgeCat = dealerCustClass.CreditLimitAgeCat;
            newCustomer.CreditLimitUsed = 1;
            newCustomer.CustClassKey = dealerCustClass.Key;
            newCustomer.DateEstab = DateTime.Now;
            newCustomer.DfltSalesAcctKey = dealerCustClass.DfltSalesAcctKey;
            newCustomer.DfltMaxUpCharge = 0;
            newCustomer.DfltMaxUpChargeType = 0;
            newCustomer.Name = "Victor Refrig";
            newCustomer.FinChgFlatAmt = dealerCustClass.FinChgFlatAmt;
            newCustomer.FinChgPct = dealerCustClass.FinChgPct;
            newCustomer.Hold = 0;
            newCustomer.PmtByNationalAcctParent = 0;
            newCustomer.PrintDunnMsg = dealerCustClass.PrintDunnMsg;
            newCustomer.ReqCreditLimit = 0;
            newCustomer.ReqPO = dealerCustClass.ReqPO;
            newCustomer.RetntPct = dealerCustClass.RetntPct;
            newCustomer.StmtCycleKey = dealerCustClass.StmtCycleKey;
            newCustomer.StmtFormKey = dealerCustClass.StmtFormKey;
            newCustomer.ShipPriority = 3;
            newCustomer.Status = 1;
            newCustomer.StmtFormKey = dealerCustClass.StmtFormKey;
            newCustomer.TradeDiscPct = dealerCustClass.TradeDiscPct;
            newCustomer.UpdateCounter = 1;
            //newCustomer.UserFld2 = accountSettings.PricePackSlip;
            //newCustomer.SalesSourceKey = accountSettings.SalesSourceKey;


            newCustomer.CustAddresses = new List<CustAddress>();
            newCustomer.CustAddresses.Add(primaryCustAddress);
            newCustomer.CustAddresses.Add(billtoCustAddress);
            newCustomer.CustAddresses.Add(shiptoCustAddress);


            var transmittals = new List<DocTransmittal>
            {
                new DocTransmittal { EMail = 0, EMailFormat = 3, Fax = 0, HardCopy = 0, IncludeCC = 0, TranType = 501},
                new DocTransmittal { EMail = 0, EMailFormat = 3, Fax = 0, HardCopy = 0, IncludeCC = 0, TranType = 502},
                new DocTransmittal { EMail = 0, EMailFormat = 3, Fax = 0, HardCopy = 0, IncludeCC = 0, TranType = 503},
                new DocTransmittal { EMail = 0, EMailFormat = 3, Fax = 0, HardCopy = 0, IncludeCC = 0, TranType = 505},
                new DocTransmittal { EMail = 0, EMailFormat = 3, Fax = 0, HardCopy = 0, IncludeCC = 0, TranType = 522},
                new DocTransmittal { EMail = 0, EMailFormat = 3, Fax = 0, HardCopy = 0, IncludeCC = 0, TranType = 801},
                new DocTransmittal { EMail = 0, EMailFormat = 3, Fax = 0, HardCopy = 0, IncludeCC = 0, TranType = 835}
            };

            newCustomer.DocTransmittals = new List<DocTransmittal>();
            newCustomer.DocTransmittals.AddRange(transmittals);

            custService.AddCustomerWithDependencies(newCustomer);
        }

        //[TestMethod]
        //public void AddCustStatusForExistingCustomer()
        //{
        //    var custStatus = new CustStatus {
        //        Key = 65868,
        //        AgeCat1Amt = 0,
        //        AgeCat2Amt = 0,
        //        AgeCat3Amt = 0,
        //        AgeCat4Amt = 0,
        //        AgeCurntAmt = 0,
        //        AgeFutureAmt = 0,
        //        AvgInvcAmt = 0,
        //        FinChgBal = 0,
        //        HighestBal = 0,
        //        LastStmtAmt = 0,
        //        LastStmtAmtNC = 0,
        //        OnSalesOrdAmt = 0,
        //        RetntBal = 0
        //    };

        //    var context = new CustomerContext();
        //    context.CustStatuses.Add(custStatus);
        //    context.SaveChanges();
        //}


        [TestMethod]
        public void LoadCustAddresses()
        {
            var service = new CustomerService();
            var customer = service.LoadCustomerWithDependenciesByKey(29301); //ECOTE92806

            Assert.IsNotNull(customer);
            Assert.AreEqual(customer.Key, 29301);
            Assert.AreEqual(customer.Id.Trim(), "ECOTE92806");
            Assert.IsTrue(customer.CustAddresses.Count > 0);
            Assert.IsTrue(customer.Contacts.Count > 0);
            Assert.IsTrue(customer.DocTransmittals.Count > 0);
            Assert.IsNotNull(customer.CustAddresses.FirstOrDefault().Address);
        }


        private Address GetHydratedAddress(string addr1)
        {
            return new Address
            {
                Line1 = addr1,
                City = "Monterey Park",
                State = "CA",
                Zip = "91754",
                Country = "USA",
                Residential = 1,
                TransactionOverride = 0,
                UpdateCounter = 1
            };
        }
        private CustAddress GetHydratedCustAddress(CustClass custClass, API.Enums.CustAddrType type)
        {
            var custAdress = new CustAddress();
            custAdress.AllowInvtSubst = custClass.AllowInvtSubst;
            custAdress.BackOrdPrice = 0;
            custAdress.BOLReqd = 0;
            custAdress.CarrierBillMeth = 6;
            custAdress.CloseSOLineOnFirstShip = 0;
            custAdress.CloseSOOnFirstShip = 0;
            custAdress.CreateDate = DateTime.Now;
            custAdress.CreateType = 0;
            custAdress.CreateUserID = Environment.UserName;
            custAdress.CurrID = custClass.CurrID;
            custAdress.CustAddrID = "Test - " + type.ToString();
            custAdress.FOBKey = custClass.FOBKey;
            custAdress.FreightMethod = 2;
            custAdress.InvoiceReqd = 0;
            custAdress.LanguageID = custClass.LanguageID;
            custAdress.PackListContentsReqd = 0;
            custAdress.PackListReqd = 0;
            custAdress.PriceAdj = 0;
            custAdress.PriceBase = 0;
            custAdress.PrintOrderAck = 0;
            custAdress.PmtTermsKey = 22;
            custAdress.RequireSOAck = custClass.RequireSOAck;
            custAdress.ShipComplete = custClass.ShipComplete;
            custAdress.ShipDays = 3;
            custAdress.ShipLabelFormKey = custClass.ShipLabelFormKey;
            custAdress.ShipLabelsReqd = 0;
            custAdress.SOAckFormKey = custClass.SOAckFormKey;
            custAdress.SOAckMeth = 0;
            custAdress.SperKey = custClass.SperKey;
            //billtoCustAddress.WhseKey = accountSettings.WarehouseKey;
            custAdress.UsePromoPrice = 0;
            custAdress.ShipMethKey = custClass.ShipMethKey;
            //billtoCustAddress.WhseKey = accountSettings.WarehouseKey;
            custAdress.InvcFormKey = custClass.InvcFormKey;
            //billtoCustAddress.PackListFormKey = accountSettings.PackListFormKey;
            //billtoCustAddress.SalesTerritoryKey = accountSettings.TerritoryKey;
            custAdress.Type = type;

            return custAdress;
        }



        [TestMethod]
        public void AddNewCustAddr()
        {
            var service = new CustomerService();
            var lookupService = new LookUpService();
            var custClass = lookupService.GetCustClasses().First();

            var custAddress = new CustAddress();
            custAddress.AllowInvtSubst = custClass.AllowInvtSubst;
            custAddress.BackOrdPrice = 0;
            custAddress.BOLReqd = 0;
            custAddress.CarrierBillMeth = 6;
            custAddress.CloseSOLineOnFirstShip = 0;
            custAddress.CloseSOOnFirstShip = 0;
            custAddress.CreateDate = DateTime.Now;
            custAddress.CreateType = 0;
            custAddress.CreateUserID = Environment.UserName;
            custAddress.CurrID = custClass.CurrID;
            custAddress.CustAddrID = "Test";
            custAddress.CustKey = 65932;
            custAddress.FOBKey = custClass.FOBKey;
            custAddress.FreightMethod = 2;
            custAddress.InvoiceReqd = 0;
            custAddress.LanguageID = custClass.LanguageID;
            custAddress.PackListContentsReqd = 0;
            custAddress.PackListReqd = 0;
            custAddress.PriceAdj = 0;
            custAddress.PriceBase = 0;
            custAddress.PrintOrderAck = 0;
            custAddress.RequireSOAck = custClass.RequireSOAck;
            custAddress.ShipComplete = custClass.ShipComplete;
            custAddress.ShipDays = 3;
            custAddress.ShipLabelFormKey = custClass.ShipLabelFormKey;
            custAddress.ShipLabelsReqd = 0;
            custAddress.SOAckFormKey = custClass.SOAckFormKey;
            custAddress.SOAckMeth = 0;
            custAddress.SperKey = custClass.SperKey;
            custAddress.WhseKey = 23;
            custAddress.UsePromoPrice = 0;
            custAddress.ShipMethKey = custClass.ShipMethKey;
            custAddress.InvcFormKey = custClass.InvcFormKey;
            custAddress.PackListFormKey = 84;
            custAddress.SalesTerritoryKey = 2;

            var address = new Address()
            {
                Name = "Dom's Test",
                Line1 = "877 Monterey Pass Rd",
                City = "Monterey Park",
                State = "CA",
                Zip = "91754",
                Country = "USA",
                Residential = 1,
                TransactionOverride = 0,
                UpdateCounter = 1
            };

            custAddress.Address = address;

            service.AddCustAddress(custAddress);

            Assert.IsTrue(true);
        }


        [TestMethod]
        public void UpdateCustomerDefaultShipTo()
        {
            var service = new CustomerService();
            Customer customer = service.LoadCustomerWithDependenciesByKey(29301);

            customer.CustAddresses.First(c => c.Key == 11399825).Address.Line2 = "Apt B";
            customer.DefaultShipToAddress = customer.CustAddresses.First(c => c.Key == 11399825).Address;
            customer.DfltShipToAddrKey = customer.CustAddresses.First(c => c.Key == 11399825).Address.Key;

            service.UpdateCustomer(customer);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void AddCustAddressAndAssignDefaultShipTo()
        {
            var service = new CustomerService();
            var lookupService = new LookUpService();
            var custClass = lookupService.GetCustClasses().First();

            var custAddress = new CustAddress();
            custAddress.AllowInvtSubst = custClass.AllowInvtSubst;
            custAddress.BackOrdPrice = 0;
            custAddress.BOLReqd = 0;
            custAddress.CarrierBillMeth = 6;
            custAddress.CloseSOLineOnFirstShip = 0;
            custAddress.CloseSOOnFirstShip = 0;
            custAddress.CreateDate = DateTime.Now;
            custAddress.CreateType = 0;
            custAddress.CreateUserID = Environment.UserName;
            custAddress.CurrID = custClass.CurrID;
            custAddress.CustAddrID = "Test";
            custAddress.CustKey = 65932;
            custAddress.FOBKey = custClass.FOBKey;
            custAddress.FreightMethod = 2;
            custAddress.InvoiceReqd = 0;
            custAddress.LanguageID = custClass.LanguageID;
            custAddress.PackListContentsReqd = 0;
            custAddress.PackListReqd = 0;
            custAddress.PriceAdj = 0;
            custAddress.PriceBase = 0;
            custAddress.PrintOrderAck = 0;
            custAddress.RequireSOAck = custClass.RequireSOAck;
            custAddress.ShipComplete = custClass.ShipComplete;
            custAddress.ShipDays = 3;
            custAddress.ShipLabelFormKey = custClass.ShipLabelFormKey;
            custAddress.ShipLabelsReqd = 0;
            custAddress.SOAckFormKey = custClass.SOAckFormKey;
            custAddress.SOAckMeth = 0;
            custAddress.SperKey = custClass.SperKey;
            custAddress.WhseKey = 23;
            custAddress.UsePromoPrice = 0;
            custAddress.ShipMethKey = custClass.ShipMethKey;
            custAddress.InvcFormKey = custClass.InvcFormKey;
            custAddress.PackListFormKey = 84;
            custAddress.SalesTerritoryKey = 2;

            var address = new Address()
            {
                Name = "Case Parts",
                Line1 = "877 Monterey Pass Rd",
                City = "Monterey Park",
                State = "CA",
                Zip = "91754",
                Country = "USA",
                Residential = 1,
                TransactionOverride = 0,
                UpdateCounter = 1
            };

            custAddress.Address = address;

            Customer customer = service.LoadCustomerWithDependenciesByKey(29301);

            service.InsertCustAddrAndAssignCustomerDefaultAddress(customer, custAddress, true, true);

            Assert.IsTrue(true);
        }


        [TestMethod]
        public void LoadNationalAccount()
        {
            var service = new CustomerService();
            var nataccount = service.GetNationalAccountByKey(589);

            Assert.IsNotNull(nataccount);
        }

        [TestMethod]
        public void LoadNationalAccountLevel()
        {
            var service = new CustomerService();
            var level = service.LoadNationalAccountLevelByKey(589);

            Assert.IsNotNull(level);
        }



        [TestMethod]
        public void AddNationalAccount()
        {
            var service = new CustomerService();
            var newHQaccount = service.LoadCustomerWithDependenciesById("DOMSR91754-1");

            service.CreateNationalAccount(newHQaccount);
        }

        [TestMethod]
        public void AddBranchToHQ()
        {
            var service = new CustomerService();
            var branch = service.LoadCustomerWithDependenciesById("DOMSR91754-4");
            var hq = service.LoadCustomerWithDependenciesById("DOMSR91754");
            service.AddBranchToNationalAccount(branch, hq);
        }


        [TestMethod]
        public void LoadCustomerWithNationalAccountLevel()
        {
            var service = new CustomerService();
            var customer = service.LoadCustomerWithDependenciesById("DOMSR89101");

            Assert.IsNotNull(customer.NationalAccountLevel);
        }

        [TestMethod]
        public void LoadNationalAccountLevelWithDependencies()
        {
            var service = new CustomerService();
            var accountLevel = service.LoadNationalAccountLevelByKey(590);

            Assert.IsNotNull(accountLevel.NationalAccount);
        }



        [TestMethod]
        public void LoadGroupByGroupIDShouldReturnData()
        {
            var service = new LookUpService();
            var group = service.GetGroupByGroupID("Nerds");

            Assert.IsNotNull(group);
            Assert.IsNotNull(group.Users.FirstOrDefault(c => c.UserID == "DomingoG"));
        }

        [TestMethod] //TODO FIX RELATIONSHIP
        public void LoadCPCTaxExemptionsThroughCustomer()
        {
            var context = new CustomerContext();
            var customer = context.Customers
                            .Include(c => c.TaxExemptionsCPC)
                            .FirstOrDefault(c => c.Key == 4544);

            Assert.IsNotNull(customer);
            Assert.IsTrue(customer.TaxExemptionsCPC.Count == 3);
        }

        [TestMethod]
        public void LoadAcuityTaxExemptionsThroughCustAddress()
        {
            var context = new CustomerContext();
            var custAddr = context.CustAddresses
                            .Include(c => c.TaxExemptionsAcuity)
                            .FirstOrDefault(c => c.Key == 14384);

            Assert.IsNotNull(custAddr);
            Assert.IsTrue(custAddr.TaxExemptionsAcuity.Count > 0);
        }

        [TestMethod]
        public void LoadTaxScheduleThroughCustAddress()
        {
            var context = new CustomerContext();
            var custAddr = context.CustAddresses
                            .Include(c => c.TaxExemptionsAcuity)
                            .FirstOrDefault(c => c.Key == 14384);

            Assert.IsNotNull(custAddr);
            Assert.IsNotNull(custAddr.TaxSchedule);
        }


        [TestMethod]
        public void LoadTaxCodesThroughTaxSchedule()
        {
            var context = new CustomerContext();
            var schedule = context.TaxSchedules
                            .Include(c => c.TaxCodes)
                            .FirstOrDefault(c => c.Key == 46);

            Assert.IsNotNull(schedule);
            Assert.IsTrue(schedule.TaxCodes.Count > 0);
        }

        [TestMethod]
        public void LoadTaxCodeThroughTaxExemptionAcuity()
        {
            var context = new CustomerContext();
            var exemption = context.TaxExemptionsAcuity
                            .Include(c => c.TaxCode)
                            .FirstOrDefault(c => c.AddrKey == 14384);

            Assert.IsNotNull(exemption);
            Assert.IsNotNull(exemption.TaxCode);
        }

        [TestMethod]
        public void LoadTaxCodesThroughCustomer()
        {
            var context = new CustomerContext();
            var customer = context.Customers
                                    .Include(c => c.CustAddresses
                                            .Select(o => o.TaxExemptionsAcuity))
                                            //.Select(ex => ex.TaxCode)))
                                     .FirstOrDefault(c => c.Id == "DOMWS91754");

            foreach (var custAddr in customer.CustAddresses)
            {
                Assert.IsTrue(custAddr.TaxExemptionsAcuity.Count == 4);
                foreach (var exemption in custAddr.TaxExemptionsAcuity)
                {
                    Assert.IsNotNull(exemption.TaxCode);
                }
            }
        }

        [TestMethod]
        public void LoadTaxExemptionsAcuityFromCustAddress()
        {
            var context = new CustomerContext();
            var custAddr = context.CustAddresses
                                    .Include(c => c.TaxExemptionsAcuity)
                                     .FirstOrDefault(c => c.Key == 11399666);

            Assert.IsTrue(custAddr.TaxExemptionsAcuity.Count == 4);

        }

        [TestMethod]
        public void LoadTaxRatesThroughCustomer()
        {
            var context = new CustomerContext();
            var customer = context.Customers
                                     .FirstOrDefault(c => c.Id == "DOMWS91754");

            foreach (var taxCode in customer.CustAddresses[0].TaxSchedule.TaxCodes)
            {
                Assert.IsTrue(taxCode.TaxSubjClass.Rate > 0);
            }
        }


        [TestMethod]
        public void GetCountryWithStatesRelationship()
        {
            var service = new LookUpService();
            var countries = service.GetAllCountries();

            Assert.IsTrue(countries.Count > 0);
            Assert.IsTrue(countries.First().States.Count > 0);
        }


        [TestMethod]
        public void TestCustID()
        {
            var service = new CustomerService();
            var matchTuple = service.SearchByCustId("doubl92694");

            Assert.IsTrue(matchTuple == null);
        }

    }
}
