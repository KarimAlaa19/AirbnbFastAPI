using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbDAL
{
    public class PropertyMetadata
    {
        [Required]
        public decimal PricePerNight { get; set; }
        [Required]
        public decimal InsuranceTax { get; set; }
        [MinLength(25)]
        [MaxLength(255)]
        public string? Description { get; set; }
        [Required]
        public Guid? CountryId { get; set; }
        [Required]
        public Guid? CityId { get; set; }
        [MinLength(3), MaxLength(50)]
        [Required]
        public string? Street { get; set; }
        //[Required]
        public string? Longitude { get; set; }
        //[Required]
        public string? Latitude { get; set; }
        [Required]
        [Range(0, 5)]
        public double Rating { get; set; }


    }
}
