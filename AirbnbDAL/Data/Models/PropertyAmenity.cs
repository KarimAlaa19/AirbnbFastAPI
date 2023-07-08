using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbDAL;

public class PropertyAmenity
{
    public Guid AmenityId { get; set; }
    public Guid PropertyId { get; set; }

    public Property? Property { get; set; }
    public Amenity? Spec { get; set; }
}
