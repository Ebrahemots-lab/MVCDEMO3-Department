using ApplicationBLL.Interfaces;
using ApplicationDAL.Data.Context;
using ApplicationDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplicationBLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class //->This ensure that the T is a class
    {
        private readonly ApplicationContext _context;

        public GenericRepository(ApplicationContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<T>> ShowAllAsync()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) _context.employees.Include(E => E.Department).AsNoTracking().ToListAsync().Result;
            }
            else if(typeof(T) == typeof(Department))
            {
                return (IEnumerable<T>)  _context.departments.Include(D => D.Employees).AsNoTracking().ToListAsync().Result;
            }

            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
           if(typeof(T) == typeof(Employee)) 
            {
                return await  _context.employees.AsNoTracking().FirstOrDefaultAsync(D => D.Id == id) as T;
            }
            else 
            {
                return await _context.Set<T>().FindAsync(id);
            }

        }

        public async Task<int> AddAsync(T item)
        {
            _context.Add(item); //This will search for the DBSET OF T and add the item to it
            return await _context.SaveChangesAsync();
        }

        public int Update(T item)
        {
            _context.Update(item);
            return _context.SaveChanges();  
        }

        public int Delete(T item)
        {
            _context.Set<T>().Remove(item);

            return _context.SaveChanges();
        }

       
    }
}
