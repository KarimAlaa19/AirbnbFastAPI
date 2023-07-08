using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbBL
{
    public class CountryDetailsDTO
    {
        public Guid CountryId { get; set; }
        public string? CountryName { get; set; }
        //public string? CouuntryPhoneCode { get; set; }
        public IEnumerable<CityReadDTO> Cities { get; set; } = new List<CityReadDTO>();
    }
}
