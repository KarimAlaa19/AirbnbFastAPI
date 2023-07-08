using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbDAL
{
    public interface IRuleRepo : IGenericRepo<Rule>
    {
        IEnumerable<Rule> GetRulesForProperty(Guid propId);
    }
}
