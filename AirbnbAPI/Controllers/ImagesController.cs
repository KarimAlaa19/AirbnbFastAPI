using AirbnbBL;
using AirbnbDAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Airbnb
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageManager _imageManager;

        public ImagesController(IImageManager imageManager)
        {
            _imageManager = imageManager;
        }

        //Handling Room images
        [HttpPost]
        public async Task<ActionResult<AddImageDTO>> UploadRoomImage([FromForm] List<IFormFile> roomImage)
        {
            List<string> urls = new List<string>();

            foreach (var roomImageItem in roomImage)
            {
                #region Extention Checking
                var extension = Path.GetExtension(roomImageItem.FileName);
                var extensionList = new string[]
                {
                ".png",
                ".jpg",
                ".jpeg",
                ".svg"
                };

                bool isExtensionAllowed = extensionList.Contains(extension,
                    StringComparer.InvariantCultureIgnoreCase);
                if (!isExtensionAllowed)
                {
                    return BadRequest("Format not allowed");
                }
                #endregion

                #region Length Checking

                bool isSizeAllowed = roomImageItem.Length is > 0 and < 5_000_000; //Picture Size (Minimum: >0 and Max: 5MB)

                if (!isSizeAllowed)
                {
                    return BadRequest("Size is Larger than allowed size");
                }
                #endregion

                #region Storing Image

                var generatedPictureName = $"{Guid.NewGuid()}{extension}";
                var savedPicturesPath = Environment.CurrentDirectory + "\\Images\\" + generatedPictureName;
                using var stream = new FileStream(savedPicturesPath, FileMode.Create);
                roomImageItem.CopyTo(stream);
                #endregion

                #region URL Generating
                var url = $"{Request.Scheme}://{Request.Host}/Images/{generatedPictureName}";
                #endregion

                urls.Add(url);
            }


            return Ok(urls);
        }

        ////Handling Profile Pictures
        //[HttpPost]
        //public ActionResult<User> Upload([FromForm] IFormFile profilePicture)
        //{
        //    #region Extention Checking
        //    var extension = Path.GetExtension(profilePicture.FileName);
        //    var extensionList = new string[]
        //    {
        //        ".png",
        //        ".jpg",
        //        ".jpeg",
        //        ".svg"
        //    };

        //    bool isExtensionAllowed = extensionList.Contains(extension,
        //        StringComparer.InvariantCultureIgnoreCase);
        //    if (!isExtensionAllowed)
        //    {
        //        return BadRequest("Format not allowed");
        //    }
        //    #endregion

        //    #region Length Checking

        //    bool isSizeAllowed = profilePicture.Length is > 0 and < 5_000_000; //Picture Size (Minimum: >0 and Max: 5MB)

        //    if (!isSizeAllowed)
        //    {
        //        return BadRequest("Size is Larger than allowed size");
        //    }
        //    #endregion

        //    #region Storing Image

        //    var generatedPictureName = $"{Guid.NewGuid()}{extension}";
        //    var savedPicturesPath = Environment.CurrentDirectory + "StaticFiles/Images/ProfilePicture" + generatedPictureName;
        //    using var stream = new FileStream(savedPicturesPath, FileMode.Create);
        //    profilePicture.CopyTo(stream);
        //    #endregion

        //    #region URL Generating
        //    var url = $"/" +
        //        $"{Request.Scheme}://{Request.Host}/StaticFiles/Images/ProfilePicture/{generatedPictureName}";
        //    #endregion

        //    return new Image(url);
        //}
    }
}
