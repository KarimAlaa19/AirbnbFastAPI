using AirbnbDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbBL
{
    public class AmenityManager : IAmenityManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public AmenityManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<ReadAmenityDTO> ReadAllAmenities()
        {
            var amenities = _unitOfWork.AmenityRepo
                .GetAll()
                .Select(amenity => new ReadAmenityDTO
                {
                    AmenityId = amenity.AmenityId,
                    Content = amenity.Content,
                    Icon = amenity.Icon
                });

            return amenities;
        }

        public async Task<bool> AddAmenity(AddAmenityDTO addAmenity)
        {
            var amenity = new Amenity
            {
                AmenityId = new Guid(),
                Content = addAmenity.Content,
                Icon = addAmenity.Icon,
            };

            _unitOfWork.AmenityRepo.Add(amenity);

            var changes = await _unitOfWork.Save();
            return changes > 0;
        }

        public IEnumerable<ReadAmenityDTO> GetAllAmenitiesForProperty(Guid propId)
        {
            var rules = _unitOfWork.AmenityRepo.GetAmenitiesForProperty(propId);

            return rules.Select(Amenity => new ReadAmenityDTO
            {
                AmenityId = Amenity.AmenityId,
                Content = Amenity.Content,
                Icon = Amenity.Icon
            });
        }
    }
}

