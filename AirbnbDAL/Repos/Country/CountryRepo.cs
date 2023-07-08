using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbDAL
{
    public class CountryRepo : GenericRepo<Country>, ICountryRepo
    {

        private readonly AirbnbContext _context;
        public CountryRepo(AirbnbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Country> GetByName(string name)
        {
            return (await _context.Set<Country>().FirstOrDefaultAsync(c => c.CountryName == name));
        }

        public Country GetCountryWithCities(Guid id)
        {
            return _context.Set<Country>().Include(c => c.Cities).FirstOrDefault(c => c.CountryId == id);
        }
    }
}
