﻿using CreateCustomer.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateCustomer.API.Contracts
{
    public interface IBusinessFormRepository : IRepository<BusinessForm>
    {
        List<BusinessForm> GetCPCBusinessForms();
    }
}
