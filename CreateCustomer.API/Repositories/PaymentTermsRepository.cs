using CreateCustomer.API.Contracts;
using CreateCustomer.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateCustomer.API.Repositories
{
    public class PaymentTermsRepository : GenericRepository<PaymentTerms>, IPaymentTermsRepository
    {
        public PaymentTermsRepository() : this(new CustomerContext()) { }

        public PaymentTermsRepository(CustomerContext context) : base(context)
        {
        }

        public ICollection<PaymentTerms> GetAllByCompanyId()
        {
            // not narrowing down due to having 58 customers with payment terms not in old list
            // having pam sort it out then will narrow down again
            return _context.Set<PaymentTerms>()
                .Where(c => c.CompanyId == "CPC" /*&& c.DiscDateOption == 1*/)
                .OrderBy(c => c.Id)
                .ToList();
        }
    }
}
