using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbDAL
{
    public class ImageMetadata
    {
        [Required]
        //[Url]
        public string? Source { get; set; }
    }
}
