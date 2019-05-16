using CreateCustomer.API.Contracts;
using CreateCustomer.API.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CreateCustomer.API.Repositories
{
    public class BusinessFormRepository : GenericRepository<BusinessForm>, IBusinessFormRepository
    {
        public BusinessFormRepository() : this(new CustomerContext())
        {

        }

        public BusinessFormRepository(CustomerContext context):base(context)
        {

        }

        public List<BusinessForm> GetCPCBusinessForms()
        {
            return _context.BusinessForms.Where(c => c.CompanyID == "CPC").ToList();
        }
    }
}
