using CreateCustomer.API.Contracts;
using CreateCustomer.API.Entities;
using CreateCustomer.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CreateCustomer.API.DomainServices
{
    public class CustomerService : IDisposable
    {
        private CustomerContext context = new CustomerContext();
        private readonly IContactRepository _contactRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IDocTransmittalRepository _docTransmittalRepository;
        private readonly ICustAddressRepository _custAddressRepository;
        private readonly ICustStatusRepository _custStatusRepository;
        private readonly INationalAccountRepository _nationalAccountRepository;
        private readonly INationalAccountLevelRepository _nationalAccountLevelRepository;
        private readonly ICustHoldRepository _custHoldRepository;
        private readonly CreditCardRepository _creditCardRepository;

        public CustomerService()
        {
            _contactRepository = new ContactRepository(context);
            _addressRepository = new AddressRepository(context);
            _customerRepository = new CustomerRepository(context);
            _docTransmittalRepository = new DocTransmittalRepository(context);
            _custAddressRepository = new CustAddressRepository(context);
            _custStatusRepository = new CustStatusRepository(context);
            _nationalAccountRepository = new NationalAccountRepository(context);
            _nationalAccountLevelRepository = new NationalAccountLevelRepository(context);
            _creditCardRepository = new CreditCardRepository(context);
            _custHoldRepository = new CustHoldRepository(context);
        }


        #region ADD CUSTOMER
        public void AddCustomerWithDependencies(Customer customer)
        {
            customer.PrimaryAddress.Key = _addressRepository.GetSurrogateKey();
            customer.PrimaryContact.Key = _contactRepository.GetSurrogateKey();
            var custKey = _customerRepository.GetSurrogateKey();
            customer.Key = custKey;

            customer.PrimaryCntctKey = customer.PrimaryContact.Key;
            customer.PrimaryAddrKey = customer.PrimaryAddress.Key;
            customer.DocTransmittals.ForEach(dt => dt.Key = customer.Key);

            customer.CustAddresses.ForEach(a => a.CustKey = customer.Key);
            customer.CustAddresses.Single(a => a.Type == Enums.CustAddrType.Primary).Key = customer.PrimaryAddress.Key;
            customer.CustAddresses.Single(a => a.Type == Enums.CustAddrType.Primary).DfltCntctKey = customer.PrimaryCntctKey;

            if (customer.DefaultBillToAddress != null) //not same as primary
            {
                customer.DefaultBillToAddress.Key = _addressRepository.GetSurrogateKey();
                customer.DfltBillToAddrKey = customer.DefaultBillToAddress.Key;

                if (customer.CustAddresses.Single(a => a.Type == Enums.CustAddrType.BillTo) != null)
                {
                    customer.CustAddresses.Single(a => a.Type == Enums.CustAddrType.BillTo).Key = customer.DefaultBillToAddress.Key;
                    customer.CustAddresses.Single(a => a.Type == Enums.CustAddrType.BillTo).CustAddrID = customer.DefaultBillToAddress.Key.ToString();
                }
            }
            else //same as primary
            {
                customer.DefaultBillToAddress = customer.PrimaryAddress;
                customer.DfltBillToAddrKey = customer.DefaultBillToAddress.Key;
            }

            if (customer.DefaultShipToAddress != null)
            {
                customer.DefaultShipToAddress.Key = _addressRepository.GetSurrogateKey();
                customer.DfltShipToAddrKey = customer.DefaultShipToAddress.Key;

                if (customer.CustAddresses.Single(a => a.Type == Enums.CustAddrType.ShipTo) != null)
                {
                    customer.CustAddresses.Single(a => a.Type == Enums.CustAddrType.ShipTo).Key = customer.DefaultShipToAddress.Key;
                    customer.CustAddresses.Single(a => a.Type == Enums.CustAddrType.ShipTo).CustAddrID = customer.DefaultShipToAddress.Key.ToString();
                }
            }
            else
            {
                customer.DefaultShipToAddress = customer.PrimaryAddress;
                customer.DfltShipToAddrKey = customer.DefaultShipToAddress.Key;
            }


            var custStatus = new CustStatus
            {
                Key = customer.Key,
                AgeCat1Amt = 0,
                AgeCat2Amt = 0,
                AgeCat3Amt = 0,
                AgeCat4Amt = 0,
                AgeCurntAmt = 0,
                AgeFutureAmt = 0,
                AvgInvcAmt = 0,
                FinChgBal = 0,
                HighestBal = 0,
                LastStmtAmt = 0,
                LastStmtAmtNC = 0,
                OnSalesOrdAmt = 0,
                RetntBal = 0
            };

            customer.CustStatus = custStatus;

            using (var scope = new TransactionScope())
            {
                try
                {
                    _customerRepository.AddCustomerWithDependecies(customer);
                    customer.PrimaryContact.CntctOwnerKey = custKey;
                    _contactRepository.Update(customer.PrimaryContact);

                    scope.Complete();
                }
                catch (Exception)
                {
                    scope.Dispose();

                    throw;
                }
            }
        }
        #endregion


        public int AddContact(Contact contact)
        {
            contact.Key = _contactRepository.GetSurrogateKey();
            _contactRepository.Add(contact);
            return contact.Key;
        }

        public void AddContacts(List<Contact> contacts)
        {
            int i = 0;
            foreach (Contact contact in contacts)
            {
                i++;
                contact.Key = _contactRepository.GetSurrogateKey();

                // 2nd paramater determines if we persist to db
                // it will persist to db on the last entity in one transaction
                _contactRepository.Add(contact, i == contacts.Count);
            }
        }

        public Contact LoadContact(int key)
        {
            return _contactRepository.GetById(key);
        }

        public List<Contact> LoadAllContactsByCustKey(int custKey)
        {
            return _contactRepository.GetAllByCustKey(custKey).ToList();
        }

        public List<Contact> LoadAllContactsByParentContactKey(int Key)
        {
            return _contactRepository.GetAllByParentContactKey(Key).ToList();
        }

        public void UpdateContact(Contact contact)
        {
            _contactRepository.Update(contact);
        }

        public void UpdateContacts(List<Contact> contacts)
        {
            int i = 0;
            foreach (Contact contact in contacts)
            {
                i++;

                // 2nd paramater determines if we persist to db
                // it will persist to db on the last entity in one transaction
                _contactRepository.Update(contact, i == contacts.Count);
            }
        }

        public void UpdateContactsWithPersist(List<Contact> contacts)
        {
            foreach (Contact contact in contacts)
            {
                _contactRepository.Update(contact);
            }
        }

        public void AddAddress(Address address)
        {
            address.Key = _addressRepository.GetSurrogateKey();
            _addressRepository.Add(address);
        }

        public Customer LoadCustomer(int key)
        {
            return _customerRepository.GetById(key);
        }

        public int AddCustomer(Customer customer)
        {
            customer.Key = _customerRepository.GetSurrogateKey();
            _customerRepository.Add(customer);
            return customer.Key;
        }

        public void UpdateCustomer(Customer customer)
        {
            _customerRepository.Update(customer);
        }

        public void UpdateExplicitlyWithDependencies(Customer customer)
        {
            _customerRepository.UpdateCustomerWithDependecies(customer);
        }

        public Customer LoadCustomerWithDependenciesById(string id)
        {
            return _customerRepository.GetWithDependenciesById(id);
        }

        public Customer LoadCustomerWithDependenciesByKey(int key)
        {
            return _customerRepository.GetWithDependenciesByKey(key);
        }

        public async Task<List<string>> GetCustIdsAsync()
        {
            return await _customerRepository.GetCustIdsAsync();
        }

        public async Task<List<string>> GetCustIdsWithQualifiersAsync()
        {
            return await _customerRepository.GetCustIdsWithQualifiersAsync();
        }

        public async Task<List<string>> GetStandAloneCustIds()
        {
            return await _customerRepository.GetStandAloneCustIDs();
        }




        #region HQ PRIMARY DOC TRANSMITTAL
        public void UpdateHQPrimaryDocTransmittal(int custKey, int tranType, bool action)
        {
            var hq = _customerRepository.GetById(custKey);
            var hqPrimary = hq.PrimaryContact;
            var hqDocTransmittal = hq.DocTransmittals.First(c => c.TranType == tranType);
            var hqCustAddress = hq.CustAddresses.First(c => c.Key == hq.PrimaryAddrKey);

            //update the hq primary contact
            switch (tranType)
            {
                case 501:
                    hqPrimary.CCInvoice = action ? (short)1 : (short)0;
                    break;

                case 502:
                    hqPrimary.CCCreditMemo = action ? (short)1 : (short)0;
                    break;

                case 503:
                    hqPrimary.CCDebitMemo = action ? (short)1 : (short)0;
                    break;

                case 522:
                    hqPrimary.CCCustStmnt = action ? (short)1 : (short)0;
                    break;
            }

            //update the hq customer's dt 
            hqDocTransmittal.EMail = action ? (short)1 : (short)0;
            hqDocTransmittal.IncludeCC = action ? (short)1 : (short)0;
            hqDocTransmittal.EMailFormat = 3;
            hqDocTransmittal.HardCopy = action ? (short)0 : (short)1;

            //update the hq customer cust addr invc form key
            hqCustAddress.InvcFormKey = action ? 145 : 79;


            //update the branches hq contacts
            foreach (var branch in hq.Branches)
            {
                var childContact = branch.Contacts.FirstOrDefault(c => c.ParentKey == hqPrimary.Key);
                if (childContact == null) continue;

                switch (tranType)
                {
                    case 501:
                        childContact.CCInvoice = action ? (short)1 : (short)0;
                        break;

                    case 502:
                        childContact.CCCreditMemo = action ? (short)1 : (short)0;
                        break;

                    case 503:
                        childContact.CCDebitMemo = action ? (short)1 : (short)0;
                        break;

                    case 522:
                        childContact.CCCustStmnt = action ? (short)1 : (short)0;
                        break;
                }

                //update the branch dt
                var branchDocTransmittal = branch.DocTransmittals.First(c => c.TranType == tranType);
                branchDocTransmittal.EMail = action ? (short)1 : (short)0;
                branchDocTransmittal.IncludeCC = action ? (short)1 : (short)0;
                branchDocTransmittal.EMailFormat = 3;
                branchDocTransmittal.HardCopy = action ? (short)0 : (short)1;

                //update the branch cust addr invc form key
                var branchCustAddr = branch.CustAddresses.First(c => c.Key == branch.PrimaryAddrKey);
                branchCustAddr.InvcFormKey = action ? 145 : 79;
            }


            //update the branches shared contacts if necessary
            if (action == false)
            {
                //turn off for everyone
                foreach (var hqContact in hq.Contacts)
                {
                    switch (tranType)
                    {
                        case 501:
                            hqContact.CCInvoice = 0;
                            break;

                        case 502:
                            hqContact.CCCreditMemo = 0;
                            break;

                        case 503:
                            hqContact.CCDebitMemo = 0;
                            break;

                        case 522:
                            hqContact.CCCustStmnt = 0;
                            break;
                    }
                }

                foreach (var branch in hq.Branches)
                {
                    foreach (var contact in branch.Contacts)
                    {
                        switch (tranType)
                        {
                            case 501:
                                contact.CCInvoice = 0;
                                break;

                            case 502:
                                contact.CCCreditMemo = 0;
                                break;

                            case 503:
                                contact.CCDebitMemo = 0;
                                break;

                            case 522:
                                contact.CCCustStmnt = 0;
                                break;
                        }
                    }
                }
            }


            _customerRepository.Update(hq);
        }
        #endregion



                
        
        #region HQ NON PRIMARY DOC TRANSMITTAL
        public void UpdateHQNonPrimaryDocTransmittal(int custKey, int tranType, bool action, int contactKey)
        {
            //    Invoice = 501,
            //    CreditMemo = 502,
            //    Statement = 522,
            //    DebitMemo = 503

            var hq = _customerRepository.GetById(custKey);
            var hqContact = hq.Contacts.First(c => c.Key == contactKey);

            switch (tranType)
            {
                case 501:
                    hqContact.CCInvoice = action ? (short)1 : (short)0;
                    break;

                case 502:
                    hqContact.CCCreditMemo = action ? (short)1 : (short)0;
                    break;

                case 503:
                    hqContact.CCDebitMemo = action ? (short)1 : (short)0;
                    break;

                case 522:
                    hqContact.CCCustStmnt = action ? (short)1 : (short)0;
                    break;
            }

            foreach (var branch in hq.Branches)
            {
                var childContact = branch.Contacts.FirstOrDefault(c => c.ParentKey == hqContact.Key);
                if (childContact == null) continue;


                switch (tranType)
                {
                    case 501:
                        childContact.CCInvoice = action ? (short)1 : (short)0;
                        break;

                    case 502:
                        childContact.CCCreditMemo = action ? (short)1 : (short)0;
                        break;

                    case 503:
                        childContact.CCDebitMemo = action ? (short)1 : (short)0;
                        break;

                    case 522:
                        childContact.CCCustStmnt = action ? (short)1 : (short)0;
                        break;
                }
            }

            _customerRepository.Update(hq);
        }
        #endregion





        public Tuple<string, string> SearchByCustId(string searchString)
        {
            return _customerRepository.SearchCustIdByString(searchString);
        }

        public Tuple<int, string, string> GetCATaxScheduleByCounty(string county)
        {
            return _custAddressRepository.GetCATaxScheduleIdByCounty(county);
        }

        public Tuple<int, string, string> GetCATaxScheduleByCity(string city)
        {
            return _custAddressRepository.GetCATaxScheduleIdByCity(city);
        }

        public Tuple<int, string, string> GetWATaxScheduleByLocationCode(string locationCode)
        {
            return _custAddressRepository.GetWATaxScheduleIdByLocationCode(locationCode);
        }

        public Tuple<int, string, string> GetTaxScheduleByZip(string zip)
        {
            return _custAddressRepository.GetTaxScheduleIdByZip(zip);
        }

        public void AddCustAddress(CustAddress custAddress)
        {
            custAddress.Address.Key = _addressRepository.GetSurrogateKey();
            custAddress.Key = custAddress.Address.Key;
            custAddress.CustAddrID = custAddress.Key.ToString();
            _custAddressRepository.Add(custAddress);

        }

        public void UpdateCustAddress(CustAddress custAddress)
        {
            _custAddressRepository.Update(custAddress);
        }

        public int InsertCustAddrAndAssignCustomerDefaultAddress(Customer customer, CustAddress custAddress, bool isPrimary, bool isShipTo)
        {
            custAddress.Address.Key = _addressRepository.GetSurrogateKey();
            custAddress.Key = custAddress.Address.Key;
            custAddress.CustAddrID = custAddress.Key.ToString();
            custAddress.DfltCntctKey = customer.PrimaryCntctKey;

            customer.CustAddresses.Add(custAddress);

            if (isPrimary)
            {
                customer.PrimaryAddress = custAddress.Address;
                customer.PrimaryAddrKey = custAddress.Address.Key;

                customer.DefaultBillToAddress = custAddress.Address;
                customer.DfltBillToAddrKey = custAddress.Address.Key;
            }

            if (isShipTo)
            {
                customer.DefaultShipToAddress = custAddress.Address;
                customer.DfltShipToAddrKey = custAddress.Address.Key;
            }

            _customerRepository.Update(customer);
            return custAddress.Key;
        }

        public List<Customer> GetCustomersByPhone(string phone)
        {
            var contacts = _contactRepository.GetContactsByPhone(phone);
            var customers = new List<Customer>();

            if (contacts.Count == 0)
            {
                return customers;
            }

            foreach (var contact in contacts)
            {
                customers.Add(contact.Customer);
            }

            return customers.GroupBy(c => c.Key).Select(c => c.First()).ToList();
        }


        public async Task<List<Address>> GetAddressesByZipCodeAsync(string zipcode)
        {
            var addresses = await _addressRepository.GetAddressesByZipCodeAsync(zipcode);

            if (addresses.Count == 0)
            {
                return addresses;
            }

            return addresses.GroupBy(c => c.Line1).Select(c => c.First()).ToList();
        }

        public NationalAccount GetNationalAccountByKey(int key)
        {
            return _nationalAccountRepository.GetById(key);
        }

        public NationalAccountLevel LoadNationalAccountLevelByKey(int key)
        {
            return _nationalAccountLevelRepository.GetById(key);
        }

        public void DeleteNationalAccountLevel(int levelKey)
        {
            var levelToDelete = _nationalAccountLevelRepository.GetById(levelKey);
            _nationalAccountLevelRepository.Delete(levelToDelete);
        }

        public CreditCard LoadCreditCardByKey(int cardKey)
        {
            return _creditCardRepository.LoadCreditCardByKey(cardKey);
        }

        public List<CreditCard> LoadCreditCardsByCustKey(int custKey)
        {
            var listOfCreditCards = new List<CreditCard>();
            var listOfCCKeys = _creditCardRepository.LoadCreditCardKeysByCustKey(custKey);
            foreach (var key in listOfCCKeys)
            {
                var cc = _creditCardRepository.LoadCreditCardByKey(key);
                if (cc.Status != 2)
                    listOfCreditCards.Add(cc);
            }

            return listOfCreditCards;
        }

        public int AddNewCreditCard(CreditCard card, int custKey)
        {
            return _creditCardRepository.AddNewCreditCard(card, custKey);
        }

        public void UpdateCreditCard(CreditCard card)
        {
            _creditCardRepository.UpdateCreditCard(card);
        }

        public void UpdateCreditCardStatusToInActive(int cardKey)
        {
            _creditCardRepository.UpdateCreditCardStatusToInActive(cardKey);
        }

        public List<Contact> GetAllContacts()
        {
            return _contactRepository.GetAll().ToList();
        }

        public List<CustHold> GetLateCustHold(string collector)
        {
            return _custHoldRepository.GetLateCustomersByCollector(collector);
        }

        public List<CustHold> GetOverCustHold(string collector)
        {
            return _custHoldRepository.GetOverCustomersByCollector(collector);
        }

        public List<CustHold> GetCreditCustHold(string collector)
        {
            return _custHoldRepository.GetCreditCustomersByCollector(collector);
        }

        public List<CustHold> GetCustomerLetters45()
        {
            return _custHoldRepository.GetCustomersOver45Days();
        }

        public List<CustHold> GetCustomerLetters60()
        {
            return _custHoldRepository.GetCustomersOver60Days();
        }

        public List<CustHold> GetCustomerLetters90()
        {
            return _custHoldRepository.GetCustomersOver90Days();
        }

        public void UpdateCustHold(CustHold custHold)
        {
            _custHoldRepository.Update(custHold);
        }



        #region CREATE / DELETE NATIONAL ACCOUNT
        public void CreateNationalAccount(Customer hq)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    var nationalAccount = new NationalAccount();
                    nationalAccount.CompanyID = "CPC";
                    nationalAccount.CreditLimit = 0;
                    nationalAccount.CreditLimitUsed = 0;
                    nationalAccount.Description = hq.Name;
                    nationalAccount.Hold = 0;
                    nationalAccount.NationalAcctID = hq.Id + "N";

                    hq.PrimaryContact.Shared = true;

                    nationalAccount.Key = _nationalAccountRepository.GetSurrogateKey();
                    _nationalAccountRepository.Add(nationalAccount);

                    var parentLevel = new NationalAccountLevel();
                    parentLevel.Key = _nationalAccountLevelRepository.GetSurrogateKey();
                    parentLevel.Description = "Parent";
                    parentLevel.NationalAcctKey = nationalAccount.Key;
                    parentLevel.NationalAcctLevel = 1;

                    _nationalAccountLevelRepository.Add(parentLevel);

                    hq.NationalAcctLevelKey = parentLevel.Key;
                    hq.AcctType = "HQ";

                    _customerRepository.Update(hq);
                    scope.Complete();
                }
                catch (Exception)
                {
                    scope.Dispose();

                    throw;
                }
            }
        }



        public void DeleteNationalAccount(Customer customer)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    foreach (var branch in customer.Branches)
                    {
                        var brLevelToDelete = branch.NationalAccountLevel;
                        _nationalAccountLevelRepository.Delete(brLevelToDelete);

                        branch.NationalAccountLevel = null;
                        branch.NationalAcctLevelKey = null;
                        branch.AcctType = "SA";

                        branch.Contacts.ForEach(c =>
                        {
                            if (c.Shared && !c.IsPrimary)
                            {
                                c.Deleted = 1;
                            }

                            c.Shared = false;
                            c.CCCreditMemo = 0;
                            c.CCCustStmnt = 0;
                            c.CCInvoice = 0;
                            c.CCDebitMemo = 0;
                        });

                        branch.DocTransmittals.ForEach(c =>
                        {
                            c.EMail = 0;
                            c.IncludeCC = 0;
                            c.HardCopy = 1;
                        });
                    }

                    customer.Contacts.ForEach(c => c.Shared = false);

                    var hqLeveltoDelete = customer.NationalAccountLevel;
                    var nationalAccountToDelete = customer.NationalAccountLevel.NationalAccount;

                    _nationalAccountLevelRepository.Delete(hqLeveltoDelete);
                    _nationalAccountRepository.Delete(nationalAccountToDelete);

                    customer.Branches.Clear();
                    customer.NationalAccountLevel = null;
                    customer.NationalAcctLevelKey = null;
                    customer.AcctType = "SA";

                    _customerRepository.Update(customer);

                    scope.Complete();
                }
                catch (Exception)
                {
                    scope.Dispose();

                    throw;
                }
            }
        }
        #endregion



        #region ADD BRANCH TO NATIONAL ACCOUNT
        public void AddBranchToNationalAccount(Customer branch, Customer headquarters)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    SetBranchDefaultProperties(branch, headquarters);
                    SetUpDocumentTransmittal(branch, headquarters);
                    SetUpPrimaryContactInBranch(branch, headquarters);
                    SetUpSharedContactsInBranch(branch, headquarters);

                    var childLevel = new NationalAccountLevel();
                    childLevel.Key = _nationalAccountLevelRepository.GetSurrogateKey();
                    childLevel.Description = "Subsidiary";
                    childLevel.NationalAcctKey = headquarters.NationalAccountLevel.NationalAccount.Key;
                    childLevel.NationalAcctLevel = 2;

                    branch.NationalAccountLevel = childLevel;
                    branch.NationalAcctLevelKey = childLevel.Key;

                    if (headquarters.Branches == null)
                        headquarters.Branches = new List<Customer>();

                    headquarters.Branches.Add(branch);

                    _customerRepository.UpdateCustomerWithDependecies(headquarters);


                    scope.Complete();
                }
                catch (Exception)
                {
                    scope.Dispose();

                    throw;
                }
            }
        }

        private void SetBranchDefaultProperties(Customer branch, Customer headquarters)
        {
            branch.PmtByNationalAcctParent = 1;
            branch.ConsolidatedStatement = 1;
            branch.BillToNationalAcctParent = 1;
            branch.UserFld1 = headquarters.UserFld1;
            branch.AcctType = "BR";
        }

        private void SetUpDocumentTransmittal(Customer branch, Customer headquarters)
        {
            foreach (var dt in headquarters.DocTransmittals)
            {
                var branchDt = branch.DocTransmittals.First(c => c.TranType == dt.TranType);

                branchDt.EMail = dt.EMail;
                branchDt.IncludeCC = dt.IncludeCC;
                branchDt.HardCopy = dt.HardCopy;
                branchDt.EMailFormat = 3;
            }

            foreach (var contact in branch.Contacts)
            {
                contact.CreditMemo = false;
                contact.DebitMemo = false;
                contact.Statement = false;
                contact.Invoice = false;
            }
        }

        private void SetUpPrimaryContactInBranch(Customer branch, Customer headquarters)
        {
            var primaryContact = headquarters.Contacts.First(c => c.IsPrimary);
            primaryContact.Shared = true;

            var branchPrimaryContact = branch.Contacts.FirstOrDefault(c => c.ParentKey == primaryContact.Key);
            if (branchPrimaryContact == null) //create new 
            {
                branchPrimaryContact = new Contact
                {
                    CCCreditMemo = primaryContact.CCCreditMemo,
                    CCCustStmnt = primaryContact.CCCustStmnt,
                    CCInvoice = primaryContact.CCInvoice,
                    CCDebitMemo = primaryContact.CCDebitMemo,
                    CntctOwnerKey = branch.Key,
                    CreateDate = DateTime.Now,
                    CreateUserID = Environment.UserName,
                    Email = primaryContact.Email,
                    EMailFormat = 3,
                    EntityType = 501,
                    Fax = primaryContact.Fax,
                    FaxExt = primaryContact.FaxExt,
                    MobilePhone = primaryContact.MobilePhone,
                    Name = primaryContact.Name,
                    Phone = primaryContact.Phone,
                    PhoneExt = primaryContact.PhoneExt,
                    Module = "AR",
                    Title = primaryContact.Title,
                    FirstName = primaryContact.FirstName,
                    LastName = primaryContact.LastName,
                    Deleted = primaryContact.Deleted,
                    ParentKey = primaryContact.Key,
                    Shared = true
                };

                branchPrimaryContact.Key = _contactRepository.GetSurrogateKey();
                _contactRepository.Add(branchPrimaryContact);
            }
            else //already exists 
            {
                branchPrimaryContact.CCCreditMemo = primaryContact.CCCreditMemo;
                branchPrimaryContact.CCCustStmnt = primaryContact.CCCustStmnt;
                branchPrimaryContact.CCInvoice = primaryContact.CCInvoice;
                branchPrimaryContact.CCDebitMemo = primaryContact.CCDebitMemo;
                branchPrimaryContact.Email = primaryContact.Email;
                branchPrimaryContact.Name = primaryContact.Name;
                branchPrimaryContact.Phone = primaryContact.Phone;
                branchPrimaryContact.MobilePhone = primaryContact.MobilePhone;
                branchPrimaryContact.Fax = primaryContact.Fax;
                branchPrimaryContact.PhoneExt = primaryContact.PhoneExt;
                branchPrimaryContact.FaxExt = primaryContact.FaxExt;
                branchPrimaryContact.UpdateDate = DateTime.Now;
                branchPrimaryContact.UpdateUserID = Environment.UserName;
                branchPrimaryContact.Deleted = primaryContact.Deleted;
                branchPrimaryContact.Shared = true;
                branchPrimaryContact.UpdateCounter = branchPrimaryContact.UpdateCounter + 1;
            }

            branch.PrimaryCntctKey = branchPrimaryContact.Key;
        }

        private void SetUpSharedContactsInBranch(Customer branch, Customer headquarters)
        {
            foreach (var sharedContact in headquarters.Contacts.Where(c => c.Shared && !c.IsPrimary))
            {
                var branchSharedContact = branch.Contacts.FirstOrDefault(c => c.ParentKey == sharedContact.Key);
                if (branchSharedContact == null) //create new 
                {
                    branchSharedContact = new Contact
                    {
                        CCCreditMemo = sharedContact.CCCreditMemo,
                        CCCustStmnt = sharedContact.CCCustStmnt,
                        CCInvoice = sharedContact.CCInvoice,
                        CntctOwnerKey = branch.Key,
                        CreateDate = DateTime.Now,
                        CreateUserID = Environment.UserName,
                        Email = sharedContact.Email,
                        EMailFormat = 3,
                        EntityType = 501,
                        Fax = sharedContact.Fax,
                        FaxExt = sharedContact.FaxExt,
                        MobilePhone = sharedContact.MobilePhone,
                        Name = sharedContact.Name,
                        Phone = sharedContact.Phone,
                        PhoneExt = sharedContact.PhoneExt,
                        Module = "AR",
                        Title = sharedContact.Title,
                        FirstName = sharedContact.FirstName,
                        LastName = sharedContact.LastName,
                        Deleted = sharedContact.Deleted,
                        ParentKey = sharedContact.Key,
                        Shared = true
                    };

                    branchSharedContact.Key = _contactRepository.GetSurrogateKey();
                    _contactRepository.Add(branchSharedContact);
                }
                else //already exists 
                {
                    branchSharedContact.CCCreditMemo = sharedContact.CCCreditMemo;
                    branchSharedContact.CCCustStmnt = sharedContact.CCCustStmnt;
                    branchSharedContact.CCInvoice = sharedContact.CCInvoice;
                    branchSharedContact.Email = sharedContact.Email;
                    branchSharedContact.Name = sharedContact.Name;
                    branchSharedContact.Phone = sharedContact.Phone;
                    branchSharedContact.MobilePhone = sharedContact.MobilePhone;
                    branchSharedContact.Fax = sharedContact.Fax;
                    branchSharedContact.PhoneExt = sharedContact.PhoneExt;
                    branchSharedContact.FaxExt = sharedContact.FaxExt;
                    branchSharedContact.UpdateDate = DateTime.Now;
                    branchSharedContact.UpdateUserID = Environment.UserName;
                    branchSharedContact.Deleted = sharedContact.Deleted;
                    branchSharedContact.Shared = true;
                    branchSharedContact.UpdateCounter = branchSharedContact.UpdateCounter + 1;
                }
            }
        }
        #endregion




        #region REMOVE BRANCH FROM NATIONAL ACCOUNT
        public void RemoveBranchFromNationalAccount(Customer hq, Customer branch)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    hq.Branches.Remove(branch);

                    _customerRepository.Update(hq);

                    var levelToDelete = _nationalAccountLevelRepository.GetById((int)branch.NationalAcctLevelKey);
                    _nationalAccountLevelRepository.Delete(levelToDelete);

                    branch.NationalAcctLevelKey = null;
                    branch.AcctType = "SA";

                    //delete shared hq contacts in the branch
                    branch.Contacts.ForEach(c =>
                    {
                        if (c.Shared && !c.IsPrimary)
                        {
                            c.Deleted = 1;
                        }

                        c.Shared = false;
                        c.CCCreditMemo = 0;
                        c.CCCustStmnt = 0;
                        c.CCInvoice = 0;
                        c.CCDebitMemo = 0;
                    });

                    branch.DocTransmittals.ForEach(c =>
                    {
                        c.EMail = 0;
                        c.IncludeCC = 0;
                        c.HardCopy = 1;
                    });

                    var activeLocalContacts = branch.Contacts.Where(c => c.ParentKey == null &&
                                                                                c.Module == "AR" &&
                                                                                !c.IsDeleted).ToList();
                    if (activeLocalContacts.Count == 1)
                    {
                        branch.PrimaryCntctKey = activeLocalContacts[0].Key;
                    }


                    _customerRepository.Update(branch);

                    scope.Complete();
                }
                catch (Exception)
                {
                    scope.Dispose();

                    throw;
                }
            }
        }

        #endregion





        #region private
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
