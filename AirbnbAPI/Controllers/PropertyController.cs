using AirbnbBL;
using AirbnbDAL;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Airbnb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyManager _propertyManage;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public PropertyController(IPropertyManager propertyManage, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _propertyManage = propertyManage;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        #region Get All Properties

        [HttpGet]
        public IActionResult GetAll()
        {

            try
            {
                var properties = _propertyManage.GetAll();


                if (properties.Count() == 0)
                {
                    Response.StatusCode = 200;
                    return new JsonResult(new
                    {
                        Message = "There is no properties added yet"
                    });
                }

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

        #endregion


        #region Get Property By Id

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetOneProperty([FromRoute] Guid id)
        {

            try
            {
                var property = _propertyManage.GetProperty(id);


                if (property is null)
                    return NotFound();


                return Ok(property);
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



        #region Add Property
        [Authorize]
        [HttpPost]
        [Route("Add-Property")]
        public async Task<IActionResult> AddProperty(PropertyAddDTO propertyDto)
        {

            try
            {
                var host = await _userManager.GetUserAsync(User);
                propertyDto.HostId = host!.Id;

                var isAdded = await _propertyManage.Add(propertyDto);

                if (!isAdded)
                {
                    Response.StatusCode = 400;
                    return new JsonResult(new
                    {
                        Message = "There Was a Problem While Adding The Property!"
                    });
                }


                if (await _userManager.IsInRoleAsync(host, UserRole.Guest.ToString().ToUpper()))
                {
                    await _userManager.AddToRoleAsync(host, UserRole.Host.ToString().ToUpper());
                    Console.WriteLine("=================>>>>>>>>>> Iam here");

                    await _userManager.RemoveFromRoleAsync(host, UserRole.Guest.ToString().ToUpper());
                }


                Response.StatusCode = 201;
                return new JsonResult(new
                {
                    Message = "Property Added Successfully"
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


        #region Filter Properties

        [HttpGet]
        [Route("Filter")]
        public IActionResult FilterProperties([FromQuery] FilterDTO filterDto)
        {

            try
            {
                var properties = _propertyManage.FilterProperties(filterDto);


                if (properties.Count() == 0)
                {
                    Response.StatusCode = 200;
                    return new JsonResult(new
                    {
                        Message = "There is no properties with these specifications"
                    });
                }

                return Ok(new
                {
                    Properties = properties,
                    Count = properties.Count()
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