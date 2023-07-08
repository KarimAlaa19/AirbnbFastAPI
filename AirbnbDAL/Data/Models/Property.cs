using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbDAL;

[ModelMetadataType(typeof(PropertyMetadata))]
public class Property
{
    public Guid PropertyId { get; set; }
    public string? PropertyType { get; set; }
    [Column(TypeName = "Money")]
    public decimal PricePerNight { get; set; }
    [Column(TypeName = "Money")]
    public decimal InsuranceTax { get; set; }
    public string? Description { get; set; }
    public int? GuestNumber { get; set; }
    [ForeignKey(nameof(Country))]
    public Guid? CountryId { get; set; }
    [ForeignKey(nameof(City))]
    public Guid? CityId { get; set; }
    public string? Street { get; set; }
    public string? Longitude { get; set; }
    public string? Latitude { get; set; }
    public double Rating { get; set; } = 0;
    public DateTime Created_At { get; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; } = false;
    
    public string? HostId { get; set; }

    public int NumberOfBeds { get; set; }
    public int NumberOfBedRooms { get; set; }
    public int NumberOfBathrooms { get; set; }

    public IEnumerable<Image> Images { get; set; } = new HashSet<Image>();


    public User User { get; set; } = null!;
    public ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();
    public ICollection<GuestReviewProperty> Reviews { get; set; } = new HashSet<GuestReviewProperty>();
    public IEnumerable<PropertyRule> PropertyRules { get; set; } = new HashSet<PropertyRule>();
    public IEnumerable<PropertyAmenity> PropertyAmenities { get; set; } = new HashSet<PropertyAmenity>();
    public Country? Country { get; set; }
    public City? City { get; set; }

}
