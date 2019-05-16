using CreateCustomer.API.Contracts;
using CreateCustomer.API.Entities;
using CreateCustomer.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateCustomer.API.DomainServices
{
    public class LookUpService : IDisposable
    {
        private CustomerContext context = new CustomerContext();
        private readonly CustClassRepository _custClassRepo;
        private readonly PaymentTermsRepository _paymentTermsRepo;
        private readonly StatementCycleRepository _statementCycleRepository;
        private readonly ShipMethodRepository _shipMethodRepository;
        private readonly BusinessFormRepository _businessFormRepository;
        private readonly SalesSourceRepository _salesSourceRepository;
        private readonly BranchRepository _branchRepository;
        private readonly SalesTerritoryRepository _salesTerritoryRepository;
        private readonly TerritoryRepository _territoryRepository;
        private readonly CountryRepository _countryRepository;
        private readonly CustomerRepository _customerRepository;
        private readonly UserRepository _userRepository;
        private readonly GroupRepository _groupRepository;
        private readonly CreditCardTypeRepository _creditCardTypeRepository;
        private readonly IStateRepository _stateRepository;
        private readonly ITaxSubjClassRepository _taxSubjClassRepository;
        private readonly ITaxScheduleRepository _taxScheduleRepository;
        private readonly BatchRepository _batchRepository;

        public LookUpService()
        {
            _custClassRepo = new CustClassRepository(context);
            _paymentTermsRepo = new PaymentTermsRepository(context);
            _statementCycleRepository = new StatementCycleRepository(context);
            _shipMethodRepository = new ShipMethodRepository(context);
            _businessFormRepository = new BusinessFormRepository(context);
            _salesSourceRepository = new SalesSourceRepository(context);
            _branchRepository = new BranchRepository(context);
            _salesTerritoryRepository = new SalesTerritoryRepository(context);
            _territoryRepository = new TerritoryRepository(context);
            _countryRepository = new CountryRepository(context);
            _customerRepository = new CustomerRepository(context);
            _userRepository = new UserRepository(context);
            _groupRepository = new GroupRepository(context);
            _creditCardTypeRepository = new CreditCardTypeRepository(context);
            _stateRepository = new StateRepository(context);
            _taxSubjClassRepository = new TaxSubjClassRepository(context);
            _taxScheduleRepository = new TaxScheduleRepository(context);
            _batchRepository = new BatchRepository();
        }

        public List<CustClass> GetCustClasses()
        {
            return _custClassRepo.GetAllByCompanyId().ToList();
        }

        public List<PaymentTerms> GetPaymentTerms()
        {
            return _paymentTermsRepo.GetAllByCompanyId().ToList();
        }

        public List<StatementCycle> GetStatementCycles()
        {
            return _statementCycleRepository.GetAllByCompanyId().ToList();
        }

        public List<ShipMethod> GetShipMethods()
        {
            return _shipMethodRepository.GetAll().ToList();
        }

        public List<BusinessForm> GetBusinessForms()
        {
            return _businessFormRepository.GetCPCBusinessForms();
        }

        public List<SalesSource> GetSalesSources()
        {
            return _salesSourceRepository.GetCPCSalesSources();
        }

        public List<Branch> GetBranches()
        {
            return _branchRepository.GetAll().ToList();
        }

        public List<SalesTerritory> GetSalesTerritories()
        {
            return _salesTerritoryRepository.GetAll().ToList();
        }

        public string GetBranchIDByState(string state)
        {
            return _territoryRepository.GetBranchIDByState(state);
        }

        public List<Country> GetAllCountries()
        {
            return _countryRepository.GetAll().ToList();
        }

        public List<string> GetDistinctInvoiceCopies()
        {
            return _customerRepository.GetDistinctInvoiceCopies();
        }

        public Group GetGroupByGroupID(string groupID)
        {
            return _groupRepository.GetGroupByGroupID(groupID);
        }

        public List<CreditCardType> GetAllCreditCardTypes()
        {
            return _creditCardTypeRepository.GetAll().ToList();
        }

        public List<string> GetStatesByCountryID(string countryId)
        {
            return _stateRepository.GetStatesByCountryID(countryId).OrderBy(c => c).ToList();
        }

        public List<TaxSubjClass> GetTaxSubjClasses()
        {
            return _taxSubjClassRepository.GetAll().ToList();
        }

        public TaxSchedule GetTaxScheduleByKey(int key)
        {
            return _taxScheduleRepository.GetById(key);
        }

        public async Task<List<Batch>> GetARBatchesAsync(string batchID)
        {
            return await _batchRepository.GetARBatchesByBatchID(batchID);
        }

        public async Task<List<Batch>> GetAPBatchesAsync(string batchID)
        {
            return await _batchRepository.GetAPBatchesAsync(batchID);
        }

        public async Task<List<Batch>> GetPOBatchesAsync(string batchID)
        {
            return await _batchRepository.GetPOBatchesAsync(batchID);
        }

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
    }
}
