
using ApplicationDAL.Models;

namespace ApplicationBLL.Interfaces
{
    public interface IDepartmentRepository
    {
        //Will hold All Operations that will be Execue into a repository
        IEnumerable<Department> GetAll();

        Department Get(int id);

        int Add(Department dept);

        int Update(Department dept);

        int Delete(Department dept);

        int Save();
    }
}
