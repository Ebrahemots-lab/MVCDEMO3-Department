using ApplicationBLL.Interfaces;
using ApplicationDAL.Data.Context;
using ApplicationDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationBLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee> , IEmployeeRepository
    {
        private readonly ApplicationContext _context;
       public EmployeeRepository(ApplicationContext context): base(context) 
        {
            _context = context; 
        }

        public IEnumerable<Employee> SearchEmps(string name)
        {
            IEnumerable<Employee> searchedEmp = _context.employees.Where(emp => emp.Name.Contains(name)).Include(D => D.Department);

            return searchedEmp;
        }
    }
}
