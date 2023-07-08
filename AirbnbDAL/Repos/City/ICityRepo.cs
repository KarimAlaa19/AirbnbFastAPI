using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbDAL
{
    public interface ICityRepo : IGenericRepo<City>
    {
        Task<City> GetByName(string name);
    }
}
