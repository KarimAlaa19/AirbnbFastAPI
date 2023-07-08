using AirbnbDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbBL
{
    public class CityManager : ICityManager
    {

        private readonly IUnitOfWork _unitOfWork;
        public CityManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddCity(CityAddDTO cityDto)
        {
            var city = await _unitOfWork.CityRepo.GetByName(cityDto.CityName);

            if (city is not null)
                return false;

            _unitOfWork.CityRepo.Add(new()
            {
                CityId = Guid.NewGuid(),
                CityName = cityDto.CityName,
                CountryId = cityDto.CountryId,
            });

            return (await _unitOfWork.Save() > 0);
        }

        public IEnumerable<CityReadDTO> GetAll()
        {
            return _unitOfWork.CityRepo.GetAll().Select(c => new CityReadDTO()
            {
                CityId = c.CityId,
                CityName = c.CityName,
            });
        }

        public async Task<CityDetailsDTO> GetOne(Guid id)
        {
            var city = await _unitOfWork.CityRepo.GetById(id);
            return (new CityDetailsDTO
            {
                CityId= city.CityId,
                CityName = city.CityName,
            });
        }
    }
}
