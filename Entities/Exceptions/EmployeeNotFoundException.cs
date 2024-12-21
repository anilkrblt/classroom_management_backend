using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;

namespace Entities.Exceptions
{
    public sealed class EmployeeNotFoundException : NotFoundException
    {
        public EmployeeNotFoundException(int employeeId)
        : base($"The employee with id: {employeeId} doesn't exist in the  database.")
        {
        }
    }
}