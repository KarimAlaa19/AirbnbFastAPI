using AirbnbDAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbBL
{
    [ModelMetadataType(typeof(RuleMetadata))]
    public class RuleAddDTO
    {
        public string? Content { get; set; }
        public string? Icon { get; set; }
    }
}
