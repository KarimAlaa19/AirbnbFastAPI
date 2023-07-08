using AirbnbBL.DTOs.Property;
using AirbnbDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbBL
{
    public class PropertyManager : IPropertyManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageManager _imageManager;

        public PropertyManager(IUnitOfWork unitOfWork,IImageManager imgManage)
        {
            _unitOfWork = unitOfWork;
            _imageManager = imgManage;
        }

        public async Task<bool> Add(PropertyAddDTO propAddDto)
        {
            var property = new Property
            {
                PropertyId = Guid.NewGuid(),
                PropertyType = propAddDto.PropertyType,
                PricePerNight = propAddDto.PricePerNight,
                InsuranceTax = propAddDto.InsuranceTax,
                Description = propAddDto.Description,
                CountryId = propAddDto.CountryId,
                CityId = propAddDto.CityId,
                Street = propAddDto.Street,
                GuestNumber = propAddDto.GuestNumber,
                Longitude = propAddDto.Longitude,
                Latitude = propAddDto.Latitude,
                HostId = propAddDto.HostId

            };

            property.PropertyAmenities = propAddDto.PropertyAmenities.Select(a => new PropertyAmenity
            {
                AmenityId = a,
                PropertyId = property.PropertyId
            }).ToList();

            property.PropertyRules = propAddDto.PropertyRules.Select(r => new PropertyRule
            {
                RuleId = r,
                PropertyId = property.PropertyId
            }).ToList();

            _unitOfWork.PropertyRepo.Add(property);

            _imageManager.AddImage(propAddDto.Images, property.PropertyId);


            var changes = await _unitOfWork.Save();

            return changes > 0;
        }

        public IEnumerable<PropertyReadDTO> FilterProperties(FilterDTO filter)
        {

            FilterData filterData = new FilterData
            {
                PropertyType = filter.PropertyType,
                MaxPrice = filter.MaxPrice,
                MinPrice = filter.MinPrice,
                City = filter.City,
                BedroomsNumber = filter.BedroomsNumber,
                BedsNumber = filter.BedsNumber,
                BathroomsNumber = filter.BathroomsNumber,
                GuestsNumber = filter.GuestsNumber,
                CheckInDate = filter.CheckInDate,
                CheckOutDate = filter.CheckOutDate,
            };


            var properties = _unitOfWork.PropertyRepo.FilterProperties(filterData);



            return properties.Select(p => new PropertyReadDTO
            {
                PropertyId = p.PropertyId,
                PropertyType = p.PropertyType,
                PricePerNight = p.PricePerNight,
                Address = $"{p.City?.CityName}, {p.Country?.CountryName}",
                Rating = p.Rating,
                Images = p.Images.Select(i => i.Source)
            });


        }

        public IEnumerable<PropertyReadDTO> GetAll()
        {
            var properties = _unitOfWork.PropertyRepo.GetPropertiesWithData().Select(p => new PropertyReadDTO
            {
                PropertyId = p.PropertyId,
                PropertyType = p.PropertyType,
                PricePerNight = p.PricePerNight,
                Address = $"{p.City?.CityName}, {p.Country?.CountryName}",
                Rating = p.Rating,
                Images = p.Images.Select(i => i.Source)
            });

            return properties;
        }

        public PropertyDetailsDTO GetProperty(Guid id)
        {
            var property = _unitOfWork.PropertyRepo.GetPropertyByWithData(id);

            if (property is null)
                return null;

            return new PropertyDetailsDTO
            {
                PropertyId = property.PropertyId,
                PropertyType = property.PropertyType,
                PricePerNight = property.PricePerNight,
                InsuranceTax = property.InsuranceTax,
                Address = $"{property.City?.CityName}, {property.Country?.CountryName}",
                GuestNumber = property.GuestNumber,
                Host = new GetUserMinimalDTO
                {
                    Id = property.HostId,
                    Name = property.User.Name,
                    ProfilePicture = property.User.ProfilePicture
                },
                Images = property.Images.Select(i => i.Source)
            };
        }


        #region Host's Reservations

        public IEnumerable<PropertyReadDTO> GetHostProperties(string hostId)
        {
            var properties = _unitOfWork.PropertyRepo.GetAllHostProperties(hostId);

            return properties.Select(p => new PropertyReadDTO
            {
                PropertyId = p.PropertyId,
                PropertyType = p.PropertyType,
                PricePerNight = p.PricePerNight,
                Address = $"{p.City.CityName}, {p.Country.CountryName}",
                Rating = p.Rating,
                Images = p.Images.Select(i => i.Source)
            });
        }

        #endregion
    }
}
