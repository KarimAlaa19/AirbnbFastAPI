using AirbnbBL;
using AirbnbDAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Airbnb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityManager _cityManager;
        private readonly ICountryManager _countryManager;

        public CityController(ICityManager cityManager, ICountryManager countryManager)
        {
            _cityManager = cityManager;
            _countryManager = countryManager;
        }


        #region Get All
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {

                var cities = _cityManager.GetAll();

                if (cities.Count() == 0)
                    return Ok("There is No City Added Yet");


                return Ok(cities);
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



        #region Get by Id
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOne(Guid id)
        {
            try
            {
                var city = await _cityManager.GetOne(id);

                if (city is null)
                    return Ok("The City wasn't found!!!");


                return Ok(city);
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



        #region Add
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] CityAddDTO cityDto)
        {
            try
            {
                if (_countryManager.GetOne(cityDto.CountryId) is null)
                {
                    Response.StatusCode = 400;
                    return new JsonResult(new
                    {
                        Message = "The Country Doesn't Exist!",
                    });
                }

                var state = await _cityManager.AddCity(cityDto);

                if (!state)
                    return BadRequest("Couldn't Add The City!!!");

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
    }
}
