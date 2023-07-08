using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbBL
{
    public class ReservationsHostDTO
    {
        public string? HostId { get; set; }
        public Guid PropertyId { get; set; }
    }
}
