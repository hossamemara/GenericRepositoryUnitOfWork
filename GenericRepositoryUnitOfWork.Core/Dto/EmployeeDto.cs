﻿using GenericRepositoryUnitOfWork.Core.Models;
using GenericRepositoryUnitOfWork.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepositoryUnitOfWork.Core.Dto
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Addresse { get; set; }
        public string Sex { get; set; }
        public int Salary { get; set; }
        public DepartmentDto Department { get; set; }
        public int DepartmentId { get; set; }
    }
}
