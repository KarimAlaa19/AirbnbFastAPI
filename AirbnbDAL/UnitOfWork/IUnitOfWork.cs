using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbDAL
{
    public interface IUnitOfWork
    {
        public IAmenityRepo AmenityRepo { get; }

        public IImageRepo ImageRepo { get; }
        public ICountryRepo CountryRepo { get; }
        public ICityRepo CityRepo { get; }
        public IRuleRepo RuleRepo { get; }
        public IPropertyRepo PropertyRepo { get; }
        public IReservationRepo ReservationRepo { get; }




        public Task<int> Save();
    }
}
