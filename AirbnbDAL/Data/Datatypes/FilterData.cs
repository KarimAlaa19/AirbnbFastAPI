using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbDAL
{
    public class FilterData
    {
        //[Range(0, 10000)]
        public decimal? MinPrice { get; set; }
        //[Range(0, 10000)]
        public decimal? MaxPrice { get; set; }
        public int? BedroomsNumber { get; set; }
        public int? BathroomsNumber { get; set; }
        public int? BedsNumber { get; set; }
        //[Range(0, 9)]
        public int? GuestsNumber { get; set; }
        public string? PropertyType { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string? City { get; set; }
    }
}
