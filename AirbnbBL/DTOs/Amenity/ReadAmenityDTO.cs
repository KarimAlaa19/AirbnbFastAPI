using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace AirbnbBL
{
    public class ReadAmenityDTO
    {
        public Guid AmenityId { get; set; }
        public string? Icon { get; set; }
        public string? Content { get; set; }
    }
}