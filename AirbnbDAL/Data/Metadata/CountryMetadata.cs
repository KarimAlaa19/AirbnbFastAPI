using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbDAL
{
    public class CountryMetadata
    {
        [Required, MinLength(3), MaxLength(50)]
        public string? CountryName { get; set; }
        //[MinLength(2), MaxLength(4)]
        //public string? CouuntryPhoneCode { get; set; }
    }
}
