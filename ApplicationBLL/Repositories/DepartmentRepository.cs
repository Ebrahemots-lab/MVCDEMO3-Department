using ApplicationBLL.Interfaces;
using ApplicationDAL.Data.Context;
using ApplicationDAL.Models;

namespace ApplicationBLL.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationContext _context;

        public DepartmentRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Department> GetAll()
        {
           return _context.departments.ToList();
        }


        public Department Get(int id)
        {
            return _context.departments.Find(id);
        }

        public int Add(Department dept)
        {
           _context.departments.Add(dept);
            return _context.SaveChanges();
        }



        public int Update(Department dept)
        {
            _context.departments.Update(dept);
            return _context.SaveChanges();
        }

        public int Delete(Department dept)
        {
            _context.departments.Remove(dept);

            return _context.SaveChanges();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
