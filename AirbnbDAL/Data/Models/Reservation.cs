using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbDAL;

public class Reservation
{
    [Required]
    public DateTime CheckInDate {  get; set; }
    [Required]
    public DateTime CheckOutDate { get; set;}
    [Required]
    public int NumberOfGuests { get; set; }
    public ReservationStatus Status { get; set; } = ReservationStatus.Pending;
    [Column(TypeName = "Money")]
    public decimal TotalPrice { get; set; }
    public DateTime Created_At { get; } = DateTime.UtcNow;


    public string? GuestId { get; set; }
    [Required]
    public Guid PropertyId { get; set; }

    public User User { get; set; } = null!;
    public Property Property { get; set; } = null!;
   



}
