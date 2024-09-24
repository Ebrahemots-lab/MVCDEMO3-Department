using ApplicationBLL.Interfaces;
using ApplicationDAL.Data.Context;
using ApplicationDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplicationBLL.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly ApplicationContext _context;

        public DepartmentRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public  async Task<Department> FindAsync(string code)
        {
            return await _context.departments.Where(D => D.Name == code).FirstOrDefaultAsync();
        }
    }
}
