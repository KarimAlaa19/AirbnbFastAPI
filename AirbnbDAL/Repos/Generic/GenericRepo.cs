using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbDAL
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {

        private readonly AirbnbContext _context;
        public GenericRepo(AirbnbContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>();
        }


        public async Task<T> GetById(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }


        public void Add(T obj)
        {
            _context.Set<T>().Add(obj);
        }


        public void Update(T obj)
        {
        }


        public void Delete(T obj)
        {
            _context.Set<T>().Remove(obj);
        }
    }
}
