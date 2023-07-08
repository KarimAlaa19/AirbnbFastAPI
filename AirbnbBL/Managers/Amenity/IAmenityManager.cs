using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbBL
{
    public interface IAmenityManager
    {
        IEnumerable<ReadAmenityDTO> ReadAllAmenities();
        Task<bool> AddAmenity(AddAmenityDTO addAmenity);

        public IEnumerable<ReadAmenityDTO> GetAllAmenitiesForProperty(Guid propId);
    }
}
