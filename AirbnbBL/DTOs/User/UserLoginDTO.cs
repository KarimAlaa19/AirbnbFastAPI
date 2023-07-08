using AirbnbDAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbBL;

public class UserLoginDTO
{
    //[RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z0-9]{5,}$")]
    public string? Email { get; set; }
    [Required, MinLength(8), MaxLength(64), DataType(DataType.Password)]
    public string? Password { get; set; }
}
