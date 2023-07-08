using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbDAL;

public interface IReservationRepo:IGenericRepo<Reservation>
{
    public IEnumerable<Reservation> GetAllForProperty(Guid PropertyId);
    public IEnumerable<Reservation> GetAllForProperty(Guid PropertyId, DateTime startDate);
    public IEnumerable<Reservation> GetAllHostReservations(string HostId);
    public IEnumerable<Reservation> GetAllGuestReservations(string guestId);

}
