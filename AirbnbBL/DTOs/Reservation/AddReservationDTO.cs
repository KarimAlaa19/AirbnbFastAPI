using AirbnbDAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbBL;

public class AddReservationDTO
{
    [Required]
    public DateTime CheckInDate { get; set; }
    [Required]
    public DateTime CheckOutDate { get; set; }
    [Required]
    public int NumberOfGuests { get; set; }
    public string? Status { get; set; } = ReservationStatus.Pending.ToString();
    public decimal TotalPrice { get; set; }
    public string? GuestId { get; set; }
    public Guid PropertyId { get; set; }
}
