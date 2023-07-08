using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbDAL
{
    public class Country
    {
        [Key]
        public Guid CountryId { get; set; }
        public string? CountryName { get; set; }
        //public string? CouuntryPhoneCode { get; set; }

        public virtual IEnumerable<City> Cities { get; set; } = new HashSet<City>();
        public IEnumerable<User> Users { get; set; } = new HashSet<User>();
        public IEnumerable<Property> Properties { get; set; } = new HashSet<Property>();
    }
}
