using AirbnbBL.DTOs.Image;
using AirbnbDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbBL
{
    public class ImageManager : IImageManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public ImageManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddImage(IEnumerable<string> sources, Guid id)
        {
            Image image;
            foreach (var src in sources)
            {
                image = new Image
                {
                    Source = src,
                    PropertyId = id
                };
                _unitOfWork.ImageRepo.Add(image);
            }
        }


        public Task<bool> AddImage(AddImageDTO addImageDTO)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReadImageDTO> GetAllImages()
        {
            var images = _unitOfWork.ImageRepo.GetAll();
            var readImageDTO = new List<ReadImageDTO>();

            foreach (var image in images)
            {
                readImageDTO.Add(new ReadImageDTO
                {
                    ImageId = image.ImageId,
                    Source = image.Source,
                });
            }

            return readImageDTO;
        }

    }
}
