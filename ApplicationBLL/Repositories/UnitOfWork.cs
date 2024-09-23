using ApplicationBLL.Interfaces;
using ApplicationDAL.Data.Context;
using ApplicationDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationBLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _contex;

        private  IDepartmentRepository _department { get; set; }

        private  IEmployeeRepository _empRepo { get; set; }

        public UnitOfWork(ApplicationContext contex)
        {
            _department = new DepartmentRepository(contex);
            _empRepo = new EmployeeRepository(contex);
            _contex = contex;
        }

        public IDepartmentRepository DepartmentRepository =>  _department;

        public IEmployeeRepository EmployeeRepository => _empRepo;

        public int Save()
        {
            return _contex.SaveChanges();
        }
    }
}
