using CreateCustomer.API.Entities;
using System.Collections.Generic;

namespace CreateCustomer.API.Contracts
{
    public interface IContactRepository : IRepository<Contact>, IAsyncRepository<Contact>
    {
        List<Contact> GetAllByCustKey(int custKey);
        List<Contact> GetAllByParentContactKey(int key);
        List<Contact> GetContactsByPhone(string phone);
    }
}
