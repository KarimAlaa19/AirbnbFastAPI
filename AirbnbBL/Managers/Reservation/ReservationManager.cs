using AirbnbBL;
using AirbnbDAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbBL;

public class ReservationManager : IReservationManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public ReservationManager(IUnitOfWork unitOfWork, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }


    #region Add Reservation
    public async Task<bool> Add(AddReservationDTO reservationDTO)
    {
        var property = await _unitOfWork.PropertyRepo.GetById(reservationDTO.PropertyId);

        if (property is null)
        {
            return false;
        }

        reservationDTO.PropertyId = property.PropertyId;

        var reservation = new Reservation
        {
            CheckInDate = reservationDTO.CheckInDate,
            CheckOutDate = reservationDTO.CheckOutDate,
            NumberOfGuests = reservationDTO.NumberOfGuests,
            TotalPrice = reservationDTO.TotalPrice,
            GuestId = reservationDTO.GuestId,
            PropertyId = reservationDTO.PropertyId,
        };


        _unitOfWork.ReservationRepo.Add(reservation);
        return await _unitOfWork.Save() > 0;
    }
    #endregion

    public IEnumerable<GetReservationsDTO> GetAll()
    {
        var reservations = _unitOfWork.ReservationRepo.GetAll().
            Select(reservation => new GetReservationsDTO
            {
                CheckInDate = reservation.CheckInDate,
                CheckOutDate = reservation.CheckOutDate,
                NumberOfGuests = reservation.NumberOfGuests,
                Status = reservation.Status,
                TotalPrice = reservation.TotalPrice
            });
        return reservations;
    }



    #region NotAvailableDates
    public IEnumerable<DateTime> NotAvailableDays(Guid propertyId)
    {
        var reservationDates = _unitOfWork.ReservationRepo.GetAllForProperty(propertyId, DateTime.Now);

        List<DateTime> reservedDays = new List<DateTime>();

        foreach (var reservation in reservationDates)
        {
            DateTime chInDate = new DateTime(reservation.CheckInDate.Year,
                                             reservation.CheckInDate.Month,
                                             reservation.CheckInDate.Day);

            while (chInDate <= reservation.CheckOutDate)
            {
                reservedDays.Add(chInDate);
                chInDate = chInDate.AddDays(1);
            }
        }

        return reservedDays;
    }
    #endregion


    #region Host's Reservations

    public IEnumerable<GetReservationsDTO> GetHostReservation(string hostId)
    {
        var reservations = _unitOfWork.ReservationRepo.GetAllHostReservations(hostId);

        return reservations.Select(r => new GetReservationsDTO
        {
            CheckInDate = r.CheckInDate,
            CheckOutDate = r.CheckOutDate,
            NumberOfGuests = r.NumberOfGuests,
            Status = r.Status,
            TotalPrice = r.TotalPrice,
            GuestId = r.User.Name
        });
    }

    #endregion


    #region Guest's Reservations

    public IEnumerable<GetReservationsDTO> GetGuestReservation(string guestId)
    {
        var reservations = _unitOfWork.ReservationRepo.GetAllGuestReservations(guestId);

        return reservations.Select(r => new GetReservationsDTO
        {
            CheckInDate = r.CheckInDate,
            CheckOutDate = r.CheckOutDate,
            NumberOfGuests = r.NumberOfGuests,
            Status = r.Status,
            TotalPrice = r.TotalPrice,
        });
    }

    #endregion
}
