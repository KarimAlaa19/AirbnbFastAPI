using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbBL.DTOs.Image
{
    public class ReadImageDTO
    {
        public Guid ImageId { get; set; }
        public string? Source { get; set; }
    }
}