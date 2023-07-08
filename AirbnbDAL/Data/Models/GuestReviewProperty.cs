using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbDAL;

[ModelMetadataType(typeof(GuestReviewPropertyMetadata))]
public class GuestReviewProperty
{
    public string? ReviewContent { get; set; }
    public double Rating { get; set; } = 0;


    public Guid PropertyId { get; set; }
    public string? GuestId { get; set; }
    public User? User { get; set; } = null!;
    public Property? Property { get; set; }

}
