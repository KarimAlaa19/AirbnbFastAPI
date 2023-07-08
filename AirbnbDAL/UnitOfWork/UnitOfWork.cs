using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbDAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AirbnbContext _context;
        public IAmenityRepo AmenityRepo { get; }
        public IImageRepo ImageRepo { get; }
        public ICountryRepo CountryRepo { get; }
        public ICityRepo CityRepo { get; }
        public IRuleRepo RuleRepo { get; }
        public IPropertyRepo PropertyRepo { get; }

        public IReservationRepo ReservationRepo { get; }

        public UnitOfWork(
            AirbnbContext context,
            IAmenityRepo amenityRepo,
            IImageRepo imageRepo,
            ICountryRepo countryRepo,
            ICityRepo cityRepo,
            IPropertyRepo propertyRepo,
            IRuleRepo ruleRepo,
            IReservationRepo reservationRepo

            )
        {
            _context = context;
            AmenityRepo = amenityRepo;
            ImageRepo = imageRepo;
            CountryRepo = countryRepo;
            CityRepo = cityRepo;
            RuleRepo = ruleRepo;
            PropertyRepo = propertyRepo;
            ReservationRepo = reservationRepo;
        }



        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
