using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbDAL;

    public class ReservationRepo:GenericRepo<Reservation>, IReservationRepo
    {
    private readonly AirbnbContext _context;

    public ReservationRepo(AirbnbContext context):base(context)
    {
        _context = context;
    }

    public IEnumerable<Reservation> GetAllForProperty(Guid PropertyId)
    {
        return _context.Set<Reservation>().Where(p => p.PropertyId == PropertyId).ToList();
    }


    public IEnumerable<Reservation> GetAllForProperty(Guid PropertyId, DateTime startDate)
    {
        return _context
            .Set<Reservation>()
            .Where(p => p.PropertyId == PropertyId)
            .Where(p => (p.CheckInDate >= startDate) || (p.CheckOutDate >= startDate))
            .ToList();
    }

    public IEnumerable<Reservation> GetAllHostReservations(string HostId)
    {
        return _context
            .Set<Reservation>()
            .Include(r => r.User)
            .Include(r => r.Property)
            .Where(r => r.Property.HostId == HostId)
            .ToList();
    }

    public IEnumerable<Reservation> GetAllGuestReservations(string guestId)
    {
        return _context
            .Set<Reservation>()
            .Where(r => r.GuestId == guestId)
            .Include(r => r.User)
            .Include(r => r.Property)
            .ToList();
    }
}

