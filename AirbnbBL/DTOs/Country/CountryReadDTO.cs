using AirbnbDAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbBL
{
    [ModelMetadataType(typeof(CountryMetadata))]
    public class CountryReadDTO
    {
        public Guid CountryId { get; set; }
        public string? CountryName { get; set; }
        //public string? CouuntryPhoneCode { get; set; }
    }
}
