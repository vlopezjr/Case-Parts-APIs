using CreateCustomer.API.Contracts;
using CreateCustomer.API.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CreateCustomer.API.Repositories
{
    public class GroupRepository : GenericRepository<Group>, IGroupRepository
    {
        public GroupRepository() : this(new CustomerContext())
        {

        }
        public GroupRepository(CustomerContext context) : base(context)
        {

        }

        public Group GetGroupByGroupID(string groupID)
        {
            return _context.Groups
                .Include(c => c.Users)
                .Where(c => c.GroupID == groupID)
                .FirstOrDefault();
        }
    }
}
