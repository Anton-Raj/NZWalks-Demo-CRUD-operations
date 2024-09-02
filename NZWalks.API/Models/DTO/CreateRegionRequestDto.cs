using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class CreateRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage ="Name shold be minimum 2 letters")]
        public string Name { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Code shold be minimum 3 letters")]
        public string Code { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
