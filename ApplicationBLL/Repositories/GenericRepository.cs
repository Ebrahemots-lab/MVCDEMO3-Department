using ApplicationBLL.Interfaces;
using ApplicationDAL.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationBLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class //->This ensure that the T is a class
    {
        private readonly ApplicationContext _context;

        public GenericRepository(ApplicationContext context)
        {
            _context = context;
        }


        public IEnumerable<T> ShowAll()
        {
            return _context.Set<T>().ToList();
        }

        public T Get(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public int Add(T item)
        {
            _context.Add(item); //This will search for the DBSET OF T and add the item to it
            return _context.SaveChanges();
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

        public int Save()
        {
           return _context.SaveChanges();
        }
    }
}
