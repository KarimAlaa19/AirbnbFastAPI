using AirbnbDAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbBL
{
    [ModelMetadataType(typeof(PropertyMetadata))]
    public class PropertyAddDTO
    {
        public string? PropertyType { get; set; }
        public decimal PricePerNight { get; set; }
        public decimal InsuranceTax { get; set; }
        public string? Description { get; set; }
        public int? GuestNumber { get; set; }
        public Guid? CountryId { get; set; }
        public Guid? CityId { get; set; }
        public string? Street { get; set; }
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
        public string? HostId { get; set; }
        public int NumberOfBeds { get; set; }
        public int NumberOfBedRooms { get; set; }
        public int NumberOfBathrooms { get; set; }
        public IEnumerable<string> Images { get; set; } = new HashSet<string>();
        public IEnumerable<Guid> PropertyRules { get; set; } = new HashSet<Guid>();
        public IEnumerable<Guid> PropertyAmenities { get; set; } = new HashSet<Guid>();
    }
}
