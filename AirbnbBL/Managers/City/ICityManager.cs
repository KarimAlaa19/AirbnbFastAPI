using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbBL
{
    public interface ICityManager
    {
        IEnumerable<CityReadDTO> GetAll();
        Task<CityDetailsDTO> GetOne(Guid id);
        Task<bool> AddCity(CityAddDTO cityDto);
    }
}
