using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbDAL
{
    public interface ICountryRepo : IGenericRepo<Country>
    {
        Task<Country> GetByName(string name);
        Country GetCountryWithCities(Guid id);
    }
}
