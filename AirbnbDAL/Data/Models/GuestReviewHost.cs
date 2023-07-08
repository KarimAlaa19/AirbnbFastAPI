using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbDAL;

[ModelMetadataType(typeof(GuestReviewHostMetadata))]
public class GuestReviewHost
{
    public string? GuestId { get; set; }
    public string? HostId {  get; set; }
    public string? ReviewContent { get; set; }
    public double Rating { get; set; } = 0;

    public User Guest { get; set; } = null!;
    public User Host {  get; set; } = null!;
}
