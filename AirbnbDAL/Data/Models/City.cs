using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbDAL
{
    public class City
    {
        [Key]
        public Guid CityId { get; set; }
        public string? CityName { get; set; }


        [ForeignKey(nameof(Country))]
        public Guid CountryId { get; set; }
        public Country? Country { get; set; }
        public IEnumerable<User> Users { get; set; } = new HashSet<User>();
        public IEnumerable<Property> Properties { get; set; } = new HashSet<Property>();
    }
}
