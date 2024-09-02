using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;
        public ImagesController(IImageRepository repository) 
        {
            this.imageRepository = repository;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);

            if(ModelState.IsValid) 
            {
                //Map the Dto to domain model
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileName = request.FileName,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileDescription = request.FileDescription,
                    FileSizeInBytes = request.File.Length
                };

                //Use repositort to upload image
                await imageRepository.Upload(imageDomainModel);
                return Ok(imageDomainModel);

            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDto request) 
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            //Check for file extension
            if(!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            //Check for file size
            if(request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "Please upload file size less than 10 MB");
            }
        }
    }
}
