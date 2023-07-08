using AirbnbDAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbBL
{
    [ModelMetadataType(typeof(AmenityMetadata))]
    public class AddAmenityDTO
    {
        public string? Icon { get; set; }
        public string? Content { get; set; }
    }
}
