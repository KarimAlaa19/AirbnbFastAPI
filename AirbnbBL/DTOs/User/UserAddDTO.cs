using AirbnbDAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbBL;

[ModelMetadataType(typeof(UserMetadata))]
public class UserAddDTO
{
    public string? Email { get; set; }
    public string? UserName { get; set; }
    public string? Name { get; set; }
    public string? SSN { get; set; }
    public string? Password { get; set; }
    public string? PhoneNumber { get; set; }

}
