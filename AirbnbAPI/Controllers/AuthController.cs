using AirbnbBL;
using AirbnbDAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AirbnbDAL;

namespace finalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(IConfiguration config, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _config = config;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        #region Register

        [HttpPost]
        [Route("Signup")]
        public async Task<ActionResult> Register(UserAddDTO userDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = 400;
                    return new JsonResult(new
                    {
                        Message = ModelState.ValidationState,
                        ValidationErrorCount = ModelState.ErrorCount,
                    });
                }

                if ((await _userManager.FindByEmailAsync(userDto.Email)) is not null)
                    return BadRequest("The Email Already Exists!!!");

                if ((await _userManager.FindByNameAsync(userDto.UserName)) is not null)
                    return BadRequest("The UserName Already Exists!!!");


                User user = new User
                {
                    Email = userDto.Email,
                    UserName = userDto.UserName,
                    Name = userDto.Name,
                    SSN = userDto.SSN,
                    PhoneNumber = userDto.PhoneNumber
                };

                if ((await _roleManager.FindByNameAsync(UserRole.Guest.ToString()) is null))
                    return BadRequest("Role Doesn't Exist!!!");

                var userResult = await _userManager.CreateAsync(user, userDto.Password);

                if (!userResult.Succeeded)
                {
                    StringBuilder errMessage = new StringBuilder("Couldn't Register The User.\n");
                    foreach (var err in userResult.Errors)
                    {
                        errMessage.AppendLine($"{err.Description}");
                    }

                    return BadRequest(errMessage.ToString());
                }


                var roleResult = await _userManager.AddToRoleAsync(user, UserRole.Guest.ToString());


                if (!roleResult.Succeeded)
                    return BadRequest("Role Problem!!!");

                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, (await _userManager.GetRolesAsync(user))[0].ToString()),
                };


                string token = GenerateToken(claims);

                Response.Headers.Add("x-auth-token", token);

                return Created("", new { Token = token });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return new JsonResult(new
                {
                    StatusCode = 500,
                    Message = $"Server Error: {ex.InnerException?.Message??ex.Message}",
                });
            }
        }

        #endregion


        #region Login
        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login(UserLoginDTO loginData)
        {
            try
            {
                var user = (await _userManager.FindByEmailAsync(loginData.Email)) ??
                             (await _userManager.FindByNameAsync(loginData.Email));

                if (user is null)
                    return BadRequest("The Email / User Name or Password is Invalid!!!");

                if(!(await _userManager.CheckPasswordAsync(user, loginData.Password)))
                    return BadRequest("The Password is Invalid!!!");



                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, (await _userManager.GetRolesAsync(user))[0].ToString()),
                };


                string token = GenerateToken(claims);

                Response.Headers.Add("x-auth-token", token);

                return Ok(new {Token = token });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return new JsonResult(new
                {
                    StatusCode = 500,
                    Message = $"Server Error: {ex.InnerException?.Message??ex.Message}",
                });
            }

        }
        #endregion


        #region Generate Token
        private string GenerateToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config.GetValue<string>("JwtSecretKey")));

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var jwt = new JwtSecurityToken(
                claims: claims,
                issuer: "AirbnbAdmin",
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: signingCredentials
                );

            return (new JwtSecurityTokenHandler()).WriteToken(jwt);
        }
        #endregion



        #region Adding Role
        [HttpPost]
        [Route("AddRole")]
        public async Task<IActionResult> AddRole(RoleAddDTO roleDto)
        {
            try
            {
                roleDto.RoleName = roleDto.RoleName.ToUpper();

                if ((roleDto.RoleName is not null) && ((await _roleManager.FindByNameAsync(roleDto.RoleName)) is not null))
                    return BadRequest("The Role Already Exists!!!");

                var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleDto.RoleName));

                return Created("", "Role Was Added Succefully.");
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

