using AirbnbDAL;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbBL.DTOs.Property
{
    public class PropertyDetailsDTO
    {
        public Guid PropertyId { get; set; }
        public string? PropertyType { get; set; }
        public decimal PricePerNight { get; set; }
        public decimal InsuranceTax { get; set; }
        public int? GuestNumber { get; set; }
        public string? Description { get; set; }
        public string? Street { get; set; }
        public double Rating { get; set; } = 0;
        public int NumberOfBeds { get; set; }
        public int NumberOfBedRooms { get; set; }
        public int NumberOfBathrooms { get; set; }


        public GetUserMinimalDTO? Host { get; set; }

        //public ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();
        public IEnumerable<string> Images { get; set; } = new HashSet<string>();
        public IEnumerable<RuleReadDTO> PropertyRules { get; set; } = new HashSet<RuleReadDTO>();
        public string? Address { get; set; }
    }
}
