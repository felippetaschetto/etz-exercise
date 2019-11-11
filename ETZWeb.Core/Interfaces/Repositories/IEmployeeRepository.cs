using ETZWeb.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETZWeb.Core.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        List<Employee> GetEmployees();
    }
}
