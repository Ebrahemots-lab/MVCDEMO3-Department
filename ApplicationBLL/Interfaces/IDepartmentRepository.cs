
using ApplicationDAL.Models;

namespace ApplicationBLL.Interfaces
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {

        Task<Department> FindAsync(string code);
    }
}
