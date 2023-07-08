using AirbnbDAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbBL
{
    [ModelMetadataType(typeof(CountryMetadata))]
    public class CountryAddDTO
    {
        public string? CountryName { get; set; }
        //public string? CouuntryPhoneCode { get; set; }
    }
}
