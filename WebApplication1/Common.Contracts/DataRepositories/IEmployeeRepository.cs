﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Data.Contracts.DataRepositories;

namespace Data.Contracts.DataRepositories
{
    public interface IEmployeeRepository : IDataRepository<Employee>
    {
    }
}
