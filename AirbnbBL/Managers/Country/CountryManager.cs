using AirbnbDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbBL
{
    public class CountryManager : ICountryManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public CountryManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Add(CountryAddDTO countryDto)
        {
            var country = await _unitOfWork.CountryRepo.GetByName(countryDto.CountryName);

            if (country is not null)
                return false;

            _unitOfWork.CountryRepo.Add(new Country
            {
                CountryId = Guid.NewGuid(),
                CountryName = countryDto.CountryName,
            });

            return (await _unitOfWork.Save()) > 0;
        }

        public IEnumerable<CountryReadDTO> GetAll()
        {
            return _unitOfWork.CountryRepo.GetAll().Select(c => new CountryReadDTO
            {
                CountryId = c.CountryId,
                CountryName = c.CountryName,
            });
        }

        public CountryDetailsDTO GetOne(Guid id)
        {
            var country = _unitOfWork.CountryRepo.GetCountryWithCities(id);

            var countryResult = new CountryDetailsDTO
            {
                CountryId = country.CountryId,
                CountryName = country.CountryName,
                Cities = country.Cities.Select(c => new CityReadDTO { 
                    CityId = c.CityId,
                    CityName = c.CityName,
                })
            };

            return countryResult;
        }
    }
}
