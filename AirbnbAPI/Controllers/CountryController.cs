using AirbnbBL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Airbnb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryManager _countryManager;

        public CountryController(ICountryManager countryManager)
        {
            _countryManager = countryManager;
        }


        #region Get All
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {

                var countries = _countryManager.GetAll();

                if (countries.Count() == 0)
                    return Ok("There is No Countries Added Yet");


                return Ok(countries);
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
        public IActionResult GetOne([FromRoute] Guid id)
        {
            try
            {
                var country = _countryManager.GetOne(id);

                if (country is null)
                    return Ok("The Country wasn't found!!!");


                return Ok(country);
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
        public async Task<IActionResult> Add([FromBody] CountryAddDTO countryDto)
        {
            try
            {


                var state = await _countryManager.Add(countryDto);

                if (!state)
                    return BadRequest("Couldn't Add The Country!!!");

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
