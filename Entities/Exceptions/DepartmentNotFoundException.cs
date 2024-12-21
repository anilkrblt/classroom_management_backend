using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class DepartmentNotFoundException : NotFoundException
    {
        public DepartmentNotFoundException(int departmentId)
         : base($"The department with id: {departmentId} doesn't exist in the  database.")
        {
        }
    }
}