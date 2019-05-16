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
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        public ContactRepository() : this(new CustomerContext()) { }

        public ContactRepository(CustomerContext context) : base(context)
        {
        }

        public List<Contact> GetAllByCustKey(int custKey)
        {
            return _context.Contacts.Where(c => c.CntctOwnerKey == custKey && c.EntityType == 501).ToList();
        }

        public List<Contact> GetAllByParentContactKey(int key)
        {
            return _context.Contacts.Where(c => c.ParentKey == key).ToList();            
        }

        public List<Contact> GetContactsByPhone(string phone)
        {
            return _context.Contacts
                .Where(c => (c.Phone.StartsWith(phone) || c.MobilePhone.StartsWith(phone)) && c.EntityType == 501)
                .Include(c => c.Customer)
                .ToList();
        }

    }
}
