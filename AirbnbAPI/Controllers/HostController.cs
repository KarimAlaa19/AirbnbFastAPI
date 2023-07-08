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
    public class HostController : ControllerBase
    {
        private readonly IReservationManager _reservationManager;
        private readonly IPropertyManager _propertyManager;

        private readonly UserManager<User> _userManager;

        public HostController(UserManager<User> userManager, IReservationManager reservationManager, IPropertyManager propertyManager)
        {
            _userManager = userManager;
            _reservationManager = reservationManager;
            _propertyManager = propertyManager;
        }

        [Authorize]
        [HttpGet]
        [Route("Reservations")]
        public async Task<IActionResult> GetAllReservations()
        {
            try
            {
                var host = await _userManager.GetUserAsync(User);


                if (host is null) return BadRequest(new JsonResult(new
                {

                    Message = "The Host is Missing!!!"
                }));


                var reservations = _reservationManager.GetHostReservation(host.Id);

                if (reservations.Count() == 0)
                    return Ok(new JsonResult(new
                    {
                        Message = "There is no Reservations Yet."
                    }));

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


        [Authorize]
        [HttpGet]
        [Route("Properties")]
        public async Task<IActionResult> GetAllProperties()
        {
            try
            {
                var host = await _userManager.GetUserAsync(User);


                if (host is null) return BadRequest(new JsonResult(new
                {

                    Message = "The Host is Missing!!!"
                }));


                var properties = _propertyManager.GetHostProperties(host.Id);

                if (properties.Count() == 0)
                    return Ok(new JsonResult(new
                    {
                        Message = "There is no Properties Yet."
                    }));

                return Ok(properties);
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
    }
}
