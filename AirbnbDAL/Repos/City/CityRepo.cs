using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbDAL
{
    public class CityRepo : GenericRepo<City>, ICityRepo
    {

        private readonly AirbnbContext _context;
        public CityRepo(AirbnbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<City> GetByName(string name)
        {
            return (await _context.Set<City>().FirstOrDefaultAsync(c => c.CityName == name));
        }
    }
}
