using AirbnbBL.DTOs.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbBL
{
    public interface IImageManager
    {
        void AddImage(IEnumerable<string> addImageDTO, Guid propId);
        Task<bool> AddImage(AddImageDTO addImageDTO);
        IEnumerable<ReadImageDTO> GetAllImages();
    }
}
