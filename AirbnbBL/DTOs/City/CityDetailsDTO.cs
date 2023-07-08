using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbBL
{
    public class CityDetailsDTO
    {
        public Guid CityId { get; set; }
        public string? CityName { get; set; }
        public Guid CountryId { get; set; }
        public string? CountryName { get; set; }
    }
}
