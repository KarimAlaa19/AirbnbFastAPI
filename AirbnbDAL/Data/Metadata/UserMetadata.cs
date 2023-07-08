using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbDAL
{
    public class UserMetadata
    {
        [MinLength(3)]
        [MaxLength(50)]
        [Required]
        public string? Name { get; set; }
        [MinLength(5)]
        [MaxLength(50)]
        [Required]
        public string? UserName { get; set; }
        [MinLength(10)]
        [MaxLength(100)]
        [EmailAddress]
        [Required]
        public string? Email { get; set; }
        [StringLength(14)]
        [RegularExpression("\\d{14}")]
        [Required]
        public string? SSN { get; set; }
        [MinLength(8), MaxLength(64)]
        [Required]
        [PasswordPropertyText]
        public string? Password { get; set; }

        public DateTime DateOfBirth { get; set; }
        [StringLength(13), Required]
        public string? PhoneNumber { get; set; }
        //[Url]
        public string? ProfilePicture { get; set; }
        [Required]
        public Guid? CountryId { get; set; }
        [Required]
        public Guid? CityId { get; set; }

        [MinLength(3)]
        [MaxLength(250)]
        [Required]
        public string? Street { get; set; }

        public string? Longitude { get; set; }
        public string Latitude { get; set; } = string.Empty;
    }
}
