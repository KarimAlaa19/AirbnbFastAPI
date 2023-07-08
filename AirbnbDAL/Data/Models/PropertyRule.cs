using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbDAL;


public class PropertyRule
{
    public Guid PropertyId { get; set; }
    public Guid RuleId { get; set; }    

    public Rule? Rule { get; set; }
    public Property? Property { get; set; }
}
