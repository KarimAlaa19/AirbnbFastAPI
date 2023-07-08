using AirbnbDAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbBL
{
    [ModelMetadataType(typeof(CityMetadata))]
    public class CityReadDTO
    {
        public Guid CityId { get; set; }
        public string? CityName { get; set; }

    }
}
