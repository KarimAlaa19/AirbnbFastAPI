using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbDAL
{
    public class AmenityRepo : GenericRepo<Amenity>, IAmenityRepo
    {

        private readonly AirbnbContext _context;
        public AmenityRepo(AirbnbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Amenity> GetAmenitiesForProperty(Guid propId)
        {
            return _context.PropertyAmenities
                 .Where(pa => pa.PropertyId == propId)
                 .Include(pa => pa.Spec)
                 .Select(r => new Amenity
                 {
                     AmenityId = r.Spec.AmenityId,
                     Content = r.Spec.Content,
                     Icon = r.Spec.Icon,
                 })
                 .ToList();
        }
    }
}
