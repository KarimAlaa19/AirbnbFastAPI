using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbDAL
{
    public class CityMetadata
    {
        [Required, MinLength(3), MaxLength(50)]
        public string? CityName { get; set; }
        [Required]
        public Guid CountryId { get; set; }

    }
}
