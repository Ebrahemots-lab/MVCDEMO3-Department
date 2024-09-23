using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationBLL.Interfaces
{
    public interface IGenericRepository<T>
    {
        //this Generic interface will hold the blueprint for all Intefaces that will Inherit from iut

        IEnumerable<T> ShowAll();

        T Get(int id);

        int Add(T item);

        int Update(T item);

        int Delete(T item);

    }
}
