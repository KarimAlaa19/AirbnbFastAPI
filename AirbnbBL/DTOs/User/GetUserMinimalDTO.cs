using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbBL;

public class GetUserMinimalDTO
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? ProfilePicture { get; set; }
    public string? PhoneNumber { get; set; }
}
