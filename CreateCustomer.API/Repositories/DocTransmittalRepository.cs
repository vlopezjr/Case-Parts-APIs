using CreateCustomer.API.Contracts;
using CreateCustomer.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateCustomer.API.Repositories
{
    public class DocTransmittalRepository : GenericRepository<DocTransmittal>, IDocTransmittalRepository
    {
        public DocTransmittalRepository() : this(new CustomerContext()) { }

        public DocTransmittalRepository(CustomerContext context) : base(context)
        {
        }
    }
}
