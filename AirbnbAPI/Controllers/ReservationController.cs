using AirbnbBL;
using AirbnbDAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Airbnb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationManager _reservationManager;
        private readonly UserManager<User> _userManager;

        public ReservationController(IReservationManager reservationManager, UserManager<User> userManager)
        {
            _reservationManager = reservationManager;
            _userManager = userManager;
        }


        #region Add Reservation
        [Authorize]
        [HttpPost]
        [Route("Add-Reservation")]
        public async Task<ActionResult> AddReservation(AddReservationDTO addReservation)
        {
            try
            {
                var guest = await _userManager.GetUserAsync(User);

                addReservation.GuestId = guest.Id;

                var reservedDays = _reservationManager.NotAvailableDays(addReservation.PropertyId);

                if ((addReservation.CheckInDate < DateTime.Now) || (addReservation.CheckOutDate < DateTime.Now) || (addReservation.CheckOutDate < addReservation.CheckInDate))
                    return BadRequest(new
                    {
                        Message = "The Date is Invalid."
                    });

                if (reservedDays.Any((d => d.Date == addReservation.CheckInDate.Date)) || reservedDays.Any((r => r.Date == addReservation.CheckOutDate.Date)))
                    return BadRequest(new
                    {
                        Message = "Couldn't Add The Reservation. There is another Reservation At That Time."
                    });



                var state = await _reservationManager.Add(addReservation);

                if (!state)
                    return BadRequest(new
                    {
                        Message = "Couldn't Add The Reservation."
                    });


                return Ok(state);

            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return new JsonResult(new
                {
                    StatusCode = 500,
                    Message = $"Server Error: {ex.InnerException?.Message ?? ex.Message}",
                });
            }
        }
        #endregion

        #region Get All
        [HttpGet]
        public ActionResult<List<GetReservationsDTO>> GetAllReservations()
        {
            try
            {
                var reservations = _reservationManager.GetAll();

                if (reservations.Count() == 0)
                    return Ok("There is no reservations added yet!!!");

                return Ok(reservations);
            }

            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return new JsonResult(new
                {
                    StatusCode = 500,
                    Message = $"Server Error: {ex.InnerException?.Message ?? ex.Message}",
                });
            }

        }
        #endregion

        #region Get Reserved Days
        [HttpGet]
        [Route("Reserved/{id}")]
        public ActionResult<IEnumerable<DateTime>> GetDays(Guid id)
        {


            try
            {
                var reservedDays = _reservationManager.NotAvailableDays(id);

                if (reservedDays.Count() == 0)
                    return Ok("There is no reservations added yet!!!");

                return Ok(reservedDays);
            }

            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return new JsonResult(new
                {
                    StatusCode = 500,
                    Message = $"Server Error: {ex.InnerException?.Message ?? ex.Message}",
                });
            }
        }
        #endregion

        #region Get All Guest Reservations
        [Authorize]
        [HttpGet]
        [Route("Reservations")]
        public async Task<IActionResult> GetAllGuestReservations()
        {
            try
            {
                var guest = await _userManager.GetUserAsync(User);


                if (guest is null) return BadRequest(new JsonResult(new
                {
                    Message = "The Guest is Missing!!!"
                }));


                var reservations = _reservationManager.GetGuestReservation(guest.Id);

                if (reservations.Count() == 0)
                    return Ok(
                        new
                        {
                            Message = "There is no Reservations Yet."
                        });

                return Ok(reservations);
            }

            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return new JsonResult(new
                {
                    StatusCode = 500,
                    Message = $"Server Error: {ex.InnerException?.Message ?? ex.Message}",
                });
            }
        }
        #endregion

    }
}
