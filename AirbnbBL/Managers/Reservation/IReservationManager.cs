using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbBL;

public interface IReservationManager
{
    Task<bool> Add(AddReservationDTO reservationDTO);
    IEnumerable<GetReservationsDTO> GetAll();
    IEnumerable<DateTime> NotAvailableDays(Guid propertyId);
    IEnumerable<GetReservationsDTO> GetHostReservation(string hostId);
    IEnumerable<GetReservationsDTO> GetGuestReservation(string guestId);
}
