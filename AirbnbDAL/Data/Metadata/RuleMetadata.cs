using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbDAL
{
    public class RuleMetadata
    {
        [MinLength(4), MaxLength(255), Required]
        public string? Content { get; set; }
        [Required]
        //[Url]
        public string? Icon { get; set; }
    }
}
