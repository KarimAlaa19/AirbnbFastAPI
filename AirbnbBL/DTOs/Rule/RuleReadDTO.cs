using AirbnbDAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbBL
{
    public class RuleReadDTO
    {
        public Guid RuleId { get; set; }
        public string? Content { get; set; }
        public string? Icon { get; set; }
        public int? Count { get; set; }
    }
}
