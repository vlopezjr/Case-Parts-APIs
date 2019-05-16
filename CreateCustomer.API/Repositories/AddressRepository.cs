using CreateCustomer.API.Contracts;
using CreateCustomer.API.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateCustomer.API.Repositories
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository() : this(new CustomerContext()) { }

        public AddressRepository(CustomerContext context) : base(context)
        {
        }

        public async Task<List<Address>> GetAddressesByZipCodeAsync(string zipcode)
        {
            return await _context.Addresses
                .Where(c => c.Zip.StartsWith(zipcode))
                .Include(c => c.CustAddress.Customer)
                .Where(c => c.CustAddress.ShipDays < 90 && c.CustAddress.Customer.Status ==1 && c.CustAddress.Customer.CompanyId == "CPC")
                .ToListAsync();
        }
    }
}
