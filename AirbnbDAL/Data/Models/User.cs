using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirbnbDAL;

[Index(nameof(Email), Name = "EmailIndex", IsUnique = true)]
//[Index(nameof(UserName), Name = "UsernameIndex", IsUnique = true)]
[Index(nameof(SSN), Name = "SSNIndex", IsUnique = true)]
[Index(nameof(PhoneNumber), Name = "PhoneNumberIndex", IsUnique = true)]

[ModelMetadataType(typeof(UserMetadata))]
public class User : IdentityUser
{
    public string? Name { get; set; }
    public string? SSN { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? ProfilePicture { get; set; }
    [ForeignKey(nameof(Country))]
    public Guid? CountryId { get; set; }
    [ForeignKey(nameof(City))]
    public Guid? CityId { get; set; }
    public string? Street { get; set; }
    public string? Longitude { get; set; }
    public string? Latitude { get; set; }
    public DateTime Created_At { get; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; } = false;

    public ICollection<Property> Properties { get; set; } = new HashSet<Property>();
    public ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();
    public ICollection<GuestReviewProperty> Reviews { get; set; } = new HashSet<GuestReviewProperty>();
    public ICollection<GuestReviewHost> HostReviews { get; set; } = new HashSet<GuestReviewHost>();
    public ICollection<GuestReviewHost> GuestReviews { get; set; } = new HashSet<GuestReviewHost>();
    public Country? Country { get; set; }
    public City? City { get; set; }


}
