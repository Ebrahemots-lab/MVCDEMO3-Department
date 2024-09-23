using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationBLL.Interfaces
{
    public interface IUnitOfWork
    {
        /*
         * we use UOW to organize our code and make all repository that deal with database in one place
         */
        public IDepartmentRepository DepartmentRepository { get; }

        public IEmployeeRepository EmployeeRepository { get; }

        int Save();
    }
}
