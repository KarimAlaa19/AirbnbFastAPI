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
    public class PropertyReadDTO
    {
        public Guid PropertyId { get; set; }
        public string? PropertyType { get; set; }
        public decimal PricePerNight { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
        public double Rating { get; set; }
        public IEnumerable<string> Images { get; set; } = new HashSet<string>();

    }
}
