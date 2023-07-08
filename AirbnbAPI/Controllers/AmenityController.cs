using AirbnbBL;
using AirbnbDAL;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace finalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmenityController : Controller
    {
        private readonly IAmenityManager _amenityManager;
        public AmenityController(IAmenityManager amenityManager)
        {
            _amenityManager = amenityManager;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<List<ReadAmenityDTO>> GetAllAmenities()
        {
            try
            {
                var amenities = _amenityManager.ReadAllAmenities();


                if (amenities.Count() == 0)
                    return Ok("There is no amenities added yet!!!");

                return Ok(amenities);
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

        //[Authorize(Policy = "Admin")]
        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult> AddAmenity(AddAmenityDTO addAmenity)
        {

            var addedSuccessfully = await _amenityManager.AddAmenity(addAmenity);
            
            if (!addedSuccessfully)
                return BadRequest("Couldn't Add The Amenity!!!");

            return Ok();
        }

        #region Get All Amenities For Property
        [HttpGet]
        [Route("PropertyAmenity/{id}")]
        public IActionResult GetAmenitiesForProperty(Guid id)
        {
            try
            {
                var amenities = _amenityManager.GetAllAmenitiesForProperty(id);
    
            if (amenities.Count() == 0)
                    return Ok(new
                    {
                        Message = "There is no amenities for this property"
                    });

                Response.StatusCode = 200;

                return new JsonResult(new
                {
                    Amenities = amenities,
                    Count = amenities.Count(),
                });
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





//try
//{

//}
//catch (Exception ex)
//{
//    Response.StatusCode = 500;
//    return new JsonResult(new
//    {
//        StatusCode = 500,
//        Message = $"Server Error: {ex.InnerException?.Message ?? ex.Message}",
//    });
//}