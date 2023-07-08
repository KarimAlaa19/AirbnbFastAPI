using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbDAL
{
    public class GuestReviewPropertyMetadata
    {
        [Required, MinLength(10), MaxLength(512)]
        public string? ReviewContent { get; set; }
        [Required]
        [Range(0, 5)]
        public double Rating { get; set; } = 0;
    }
}
