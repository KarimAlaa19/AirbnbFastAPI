using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbDAL
{
    public class RoomMetadata
    {
        public string RoomType { get; set; }
        //custom validation based on room
        public int NumberOfBeds { get; set; }

        //custom validation based on being valid
        public string? PropertyID { get; set; }
    }
}
