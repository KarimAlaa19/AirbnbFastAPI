using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbDAL
{
    public interface IGenericRepo<T>
    {

        IEnumerable<T> GetAll();
        Task<T> GetById(Guid id);
        void Add(T obj);
        void Update(T obj);
        void Delete(T obj);

    }
}
