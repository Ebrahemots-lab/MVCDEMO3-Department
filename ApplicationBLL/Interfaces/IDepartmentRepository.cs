
using ApplicationDAL.Models;

namespace ApplicationBLL.Interfaces
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {

        Department Find(string code);
    }
}
