using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbBL
{
    public interface ICountryManager
    {
        IEnumerable<CountryReadDTO> GetAll();
        CountryDetailsDTO GetOne(Guid id);
        Task<bool> Add(CountryAddDTO countryDto);

    }
}
