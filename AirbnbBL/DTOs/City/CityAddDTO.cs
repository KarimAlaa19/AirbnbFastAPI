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
    public class CityAddDTO
    {
        public string? CityName { get; set; }
        public Guid CountryId { get; set; }

    }
}
