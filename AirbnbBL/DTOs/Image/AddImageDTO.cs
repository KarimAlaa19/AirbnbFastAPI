using AirbnbDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbBL
{
    public class AddImageDTO
    {
        public AddImageDTO(string _source)
        {
            Source = _source;
        }
        public string? Source { get; set; }

    }
}
