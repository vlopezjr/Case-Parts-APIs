using CreateCustomer.API.Contracts;
using CreateCustomer.API.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CreateCustomer.API.Repositories
{
    public class UserRepository : GenericRepository<User> , IUserRepository
    {
        public UserRepository() : this(new CustomerContext())
        {

        }
        public UserRepository(CustomerContext context) : base(context)
        {

        }
    }
}
