using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbDAL
{
    public interface IPropertyRepo : IGenericRepo<Property>
    {
        IEnumerable<Property> GetPropertiesWithData();
        Property GetPropertyByWithData(Guid id);

        IEnumerable<Property> FilterProperties(FilterData filter);

        public IEnumerable<Property> GetAllHostProperties(string HostId);
    }
}
