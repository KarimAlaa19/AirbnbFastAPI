using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbDAL
{
    public class PropertyRepo : GenericRepo<Property>, IPropertyRepo
    {
        private readonly AirbnbContext _context;
        public PropertyRepo(AirbnbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Property> FilterProperties(FilterData filter)
        {
            var queries = _context.Properties
                .Include(p => p.City)
                .Include(p => p.Country)
                .Include(p => p.Rooms)
                    .ThenInclude(r => r.Images)
                .Include(p => p.Reservations)
                 .AsQueryable();


            #region Prepare Filtertion

            if (filter.PropertyType is not null)
                queries = queries.Where((Property p) => p.PropertyType == filter.PropertyType);

            if (filter.MaxPrice is not null)
                queries = queries.Where((Property p) => p.PricePerNight <= filter.MaxPrice);

            if (filter.MinPrice is not null)
                queries = queries.Where((Property p) => p.PricePerNight >= filter.MinPrice);

            if (filter.GuestsNumber is not null)
                queries = queries.Where((Property p) => p.GuestNumber >= filter.GuestsNumber);

            if (filter.BedsNumber is not null)
                queries = queries.Where(p => p.Rooms.Sum(r => r.NumberOfBeds) >= filter.BedsNumber);

            if (filter.BedroomsNumber is not null)
                queries = queries.Where(p => p.Rooms.Where(r => r.RoomType == RoomType.Bedroom.ToString()).Count() >= filter.BedroomsNumber);

            if (filter.BathroomsNumber is not null)
                queries = queries.Where(p => p.Rooms.Where(r => r.RoomType == RoomType.Bathroom.ToString()).Count() >= filter.BathroomsNumber);


            if (filter.City is not null)
                queries = queries.Where(p => p.City.CityName.Contains(filter.City));


            if (filter.CheckInDate != null)
                queries = queries.Where(p => p.Reservations.Any(r =>
                !((filter.CheckInDate >= r.CheckInDate) &&
                 (filter.CheckInDate <= r.CheckOutDate))
                 ));

            if (filter.CheckOutDate != null)
                queries = queries.Where(p => p.Reservations.Any(r =>
                !((filter.CheckOutDate >= r.CheckInDate) &&
                 (filter.CheckOutDate <= r.CheckOutDate))
                 ));
            #endregion


            var property = queries
                .ToList()
                .OrderByDescending(p => p.Rating);

            return property;
        }

        public IEnumerable<Property> GetPropertiesWithData()
        {
            return _context.Set<Property>()
                .Include(p => p.City)
                .Include(p => p.Country)
                .Include(p => p.Rooms)
                    .ThenInclude(r => r.Images);
        }

        public Property GetPropertyByWithData(Guid id)
        {
            return _context
                .Set<Property>()
                .Where(p => p.PropertyId == id)
                .Include(p => p.User)
                .Include(p => p.PropertyRules)
                .Include(p => p.Rooms)
                    .ThenInclude(r => r.Images)
                .Include(p => p.Country)
                .Include(p => p.City)
                .First(p => p.PropertyId == id);
        }


        public IEnumerable<Property> GetAllHostProperties(string HostId)
        {
            return _context
                .Set<Property>()
                .Where(p => p.HostId == HostId)
                .Include(p => p.Country)
                .Include(p => p.City)
                .ToList();
        }
    }
}
