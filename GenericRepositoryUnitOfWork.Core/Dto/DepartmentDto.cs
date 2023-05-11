using GenericRepositoryUnitOfWork.Core.Models;
using GenericRepositoryUnitOfWork.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepositoryUnitOfWork.Core.Dto
{
    public class DepartmentDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<EmployeeList> Employees { get; set; }
    }
}
