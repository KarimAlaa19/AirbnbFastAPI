using AirbnbBL.DTOs.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbBL
{
    public interface IPropertyManager
    {

        IEnumerable<PropertyReadDTO> GetAll();
        IEnumerable<PropertyReadDTO> FilterProperties(FilterDTO filter);
        PropertyDetailsDTO GetProperty(Guid id);
        IEnumerable<PropertyReadDTO> GetHostProperties(string hostId);
        Task<bool> Add(PropertyAddDTO propAddDto);

    }
}
