using ApplicationBLL.Interfaces;
using ApplicationDAL.Data.Context;
using ApplicationDAL.Models;

namespace ApplicationBLL.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly ApplicationContext _context;

        public DepartmentRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public Department Find(string code)
        {
            return _context.departments.Where(D => D.Name == code).FirstOrDefault();
        }
    }
}
