using AutoMapper;

using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            //For Region Dto and Region
            CreateMap<Region, RegionDto>().ReverseMap();

            CreateMap<CreateRegionRequestDto, Region>().ReverseMap();

            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();

            //For DifficultyDto and Difficulty
            CreateMap<DifficultyDto, Difficulty>().ReverseMap();


            //For WalkDto and Walk
            CreateMap<CreateWalkRequestDto, Walk>().ReverseMap();

            CreateMap<Walk, WalkDto>().ReverseMap();

            CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();

        }
    }
}
