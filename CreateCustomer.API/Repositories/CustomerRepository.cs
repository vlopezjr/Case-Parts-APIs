using CreateCustomer.API.Contracts;
using CreateCustomer.API.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CreateCustomer.API.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository() : this(new CustomerContext()) { }

        public CustomerRepository(CustomerContext context) : base(context)
        {
        }

        public async Task<List<string>> GetCustIdsAsync()
        {
            var custIds = await _context.Set<Customer>().AsNoTracking().Where(c => c.CompanyId == "CPC" && c.Status == 1).OrderBy(c => c.Id).Select(c => c.Id.Trim()).ToListAsync();
            return custIds;      
        }


        public async Task<List<string>> GetStandAloneCustIDs()
        {
            return await _context.Set<Customer>()
                .Include(c => c.NationalAccountLevel)
                .Where(c => c.CompanyId == "CPC" && c.NationalAcctLevelKey == null && c.Status == 1)
                .OrderBy(c => c.Id)
                .Select(c => c.Id.TrimEnd())
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<string>> GetCustIdsWithQualifiersAsync()
        {
            return await _context.Database.SqlQuery<string>(@"
                                    SELECT 
                                    CASE
                                     WHEN NationalAcctLevel is null THEN CustID + ' - SA' 
                                     WHEN NationalAcctLevel = 1 THEN CustID + ' - HQ'
                                     WHEN NationalAcctLevel = 2 THEN CustID + ' - BR'
                                    END AS CustIDWithQualifier
                                     FROM tarCustomer FULL OUTER JOIN tarNationalAcctLevel ON tarNationalAcctLevel.NationalAcctLevelKey = tarCustomer.NationalAcctLevelKey
                                     WHERE Status = 1
                                     ORDER BY CustIDWithQualifier ASC
                                    ").ToListAsync();
        }


        public Customer GetWithDependenciesById(string id)
        {
            var customer = _context.Customers
                .Include("Contacts")
                .Include("DocTransmittals")
                .Include("PrimaryAddress")
                .Include("DefaultBillToAddress")
                .Include("DefaultShipToAddress")
                .Include("PrimaryContact")
                .Include("CustStatus")
                .Include(c => c.NationalAccountLevel.NationalAccount)
                .Include(x => x.CustAddresses.Select(y => y.Address)) //this is an Include("CustAddresses").ThenInclude("Address")
                .Include(c => c.CustAddresses.Select(x => x.TaxExemptionsAcuity))
                .Include(c => c.CustAddresses.Select(x => x.TaxSchedule.TaxCodes))
                .Where(c => c.Id == id).FirstOrDefault();

            _context.Entry(customer).Reload();

            return customer;
        }


        public Customer GetWithDependenciesByKey(int key)
        {
            var customer =  _context.Customers
                .Include("Contacts")
                .Include("DocTransmittals")
                .Include("PrimaryAddress")
                .Include("DefaultBillToAddress")
                .Include("DefaultShipToAddress")
                .Include("NationalAccountLevel")
                .Include("PrimaryContact")
                .Include(c => c.NationalAccountLevel.NationalAccount)
                .Include(x => x.CustAddresses.Select(y => y.Address)) //this is an Include("CustAddresses").ThenInclude("Address")
                .Include(c => c.CustAddresses.Select(x => x.TaxExemptionsAcuity))
                .Include(c => c.CustAddresses.Select(x=> x.TaxSchedule.TaxCodes))
                .Where(c => c.Key == key).FirstOrDefault();

            _context.Entry(customer).Reload();

            return customer;
        }

        public void AddCustomerWithDependecies(Customer customer)
        {
            _context.Entry(customer.PrimaryAddress).State = EntityState.Added;

            Add(customer);           
        }

        public void UpdateCustomerWithDependecies(Customer customer)
        {            
            foreach (var contact in customer.Contacts)
                _context.Entry(contact).State = contact.Key == 0 ? EntityState.Added : EntityState.Modified;

            foreach (var docs in customer.DocTransmittals)
                _context.Entry(docs).State = docs.Key == 0 ? EntityState.Added : EntityState.Modified;

            foreach (var branch in customer.Branches)
                _context.Entry(branch).State = branch.Key == 0 ? EntityState.Added : EntityState.Modified;

            Update(customer);
        }

        
        public void UpdateHQNonPrimaryContactDocTransmittal(int key, int tranType, bool action, int contactKey)
        {
            var parameterArray = new SqlParameter[]
            {
                new SqlParameter("@parentCustKey", key),
                new SqlParameter("@tranType", tranType),
                new SqlParameter("@action", Convert.ToInt16(action)),
                new SqlParameter("@cntctKey", contactKey),
            };

            _context.Database.ExecuteSqlCommand("spcpcSetHQDocTransmittal @parentCustKey, @tranType, @action, @cntctKey", parameterArray);
        }

        public void UpdateHQPrimaryDocTransmittal(int key, int tranType, bool action)
        {
            var parameterArray = new SqlParameter[]
            {
                new SqlParameter("@parentCustKey", key),
                new SqlParameter("@tranType", tranType),
                new SqlParameter("@action", Convert.ToInt16(action))
            };

            _context.Database.ExecuteSqlCommand("spcpcSetHQDocTransmittal @parentCustKey, @tranType, @action", parameterArray);     
        }

        public Tuple<string, string> SearchCustIdByString(string searchString)
        {
            return _context.Set<Customer>()
                .AsNoTracking()
                .Where(c => c.Id.StartsWith(searchString.Trim()) && c.CompanyId == "CPC" && c.Status == 1)
                .OrderByDescending(c => c.Id)
                .Select(c => new { Id = c.Id.Trim(), Name = c.Name.Trim() })
                .AsEnumerable()
                .Select(c => new Tuple<string, string>(c.Id, c.Name))
                .FirstOrDefault();
        }

        public List<string> GetDistinctInvoiceCopies()
        {
            return _context.Customers.Select(c => c.UserFld3).Distinct().ToList();
        }
    }
}
