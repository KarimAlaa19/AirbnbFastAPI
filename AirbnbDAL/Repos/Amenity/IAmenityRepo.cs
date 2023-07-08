using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbDAL
{
    public interface IAmenityRepo : IGenericRepo<Amenity>
    {
        IEnumerable<Amenity> GetAmenitiesForProperty(Guid propId);
    }
}
