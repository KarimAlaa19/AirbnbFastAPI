using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AirbnbDAL;


[ModelMetadataType(typeof(AmenityMetadata))]
public class Amenity
{
    public Guid AmenityId { get; set; }
    public string? Icon { get; set; }
    public string? Content { get; set; }
    public ICollection<PropertyAmenity> PropertyAmenities { get; set; } = new HashSet<PropertyAmenity>();

}
