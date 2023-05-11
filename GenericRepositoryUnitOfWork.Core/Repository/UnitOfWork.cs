using GenericRepositoryUnitOfWork.Core.Interface;
using GenericRepositoryUnitOfWork.DAL.Context;
using GenericRepositoryUnitOfWork.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepositoryUnitOfWork.Core.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGenericRepository<Employee> Employees { get; private set; }

        public IGenericRepository<Department> Departments { get; private set; }
        public UnitOfWork(ApplicationDbContext context) 
        {
           _context = context;
           Employees = new GenericRepository<Employee>(_context);
           Departments = new GenericRepository<Department>(_context);

        }
        

        public int Complete()
        {
            return _context.SaveChanges();

        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
