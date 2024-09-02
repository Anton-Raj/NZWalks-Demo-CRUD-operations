using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionsController : ControllerBase
    { 
        //For repository
        private readonly IRegionRepository regionRepository;

        //For AutoMapper
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository repository, IMapper map)
        {
            this.regionRepository = repository;

            this.mapper = map;
        }

        //Get All Regions
        [HttpGet]
        //For Authentication
        //[Authorize(Roles ="Writer")]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery]int pageNumber=1, [FromQuery]int pageSize=1000)
        {
            //Get Data from DB
            var regionDomain = await regionRepository.GetAllAsync(filterOn,filterQuery,sortBy,isAscending??true,pageNumber,pageSize);

            //Map the RegionsDomain to RegionDto
            var regionDto = mapper.Map<List<RegionDto>>(regionDomain);
            
            return Ok(regionDto);
        }

        //Get Region by ID
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            //Get data from DB
            var region = await regionRepository.GetByIdAsync(id);

            if(region == null)
            {
                return NotFound();
            }

            //Map Region Domain to region dto
            var regionDto = mapper.Map<RegionDto>(region);

            return Ok(regionDto);

            //return Ok(region);
        }

        //Create Region
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> create([FromBody] CreateRegionRequestDto createRegion)
        {
            //Map the CreateRegionRequestDto to Region Domain model
            var regionDomain = mapper.Map<Region>(createRegion);

            //create the region in DB using domain model
            regionDomain = await regionRepository.CreateAsync(regionDomain);

            //Map the region domain model to region dto for user output
            var regionDto = mapper.Map<RegionDto>(regionDomain);

            return Ok(regionDto);

            //return the createAtAction which will send 201 response to the client
            //return CreatedAtAction(nameof(GetById), new { Id = regionDto.Id }, regionDto);
        }

        //Update the Region
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody]UpdateRegionRequestDto updateregion)
        {
            //Map update region to region domain model
            var regionDomain = mapper.Map<Region>(updateregion);

            regionDomain = await regionRepository.UpdateAsync(id,regionDomain);

            if(regionDomain == null)
            {
                return NotFound();
            }

            //map the region domain model to  region dto
            var regionDto = mapper.Map<RegionDto>(regionDomain);

            return Ok(regionDto);
        }

        //Delete the Region
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id) 
        {
            var regionDomain = await regionRepository.DeleteAsync(id);

            if(regionDomain == null)
            {
                return NotFound();
            }
     
            return Ok($"{regionDomain.Name}  Region Deleted Successfully ");
        }


    }
}




//Create Region with Model validation
//[HttpPost]
//public async Task<IActionResult> create([FromBody] CreateRegionRequestDto createRegion)
//{
//    if (ModelState.IsValid)
//    {
//        //Map the CreateRegionRequestDto to Region Domain model
//        var regionDomain = mapper.Map<Region>(createRegion);

//        //create the region in DB using domain model
//        regionDomain = await regionRepository.CreateAsync(regionDomain);

//        //Map the region domain model to region dto for user output
//        var regionDto = mapper.Map<RegionDto>(regionDomain);

//        return Ok(regionDto);
//    }
//    else
//    {
//        return BadRequest(ModelState);
//    }


//    //return the createAtAction which will send 201 response to the client
//    //return CreatedAtAction(nameof(GetById), new { Id = regionDto.Id }, regionDto);
//}



//Without AutoMapper

//public class RegionsController : ControllerBase
//{
//    private readonly NZWalksDbContext dbContext;
//    private readonly IRegionRepository regionRepository;
//    public RegionsController(NZWalksDbContext dbcontext, IRegionRepository repository)
//    {
//        this.regionRepository = repository;
//        this.dbContext = dbcontext;
//    }

//    //Get All Regions
//    [HttpGet]
//    public async Task<IActionResult> GetAll()
//    {
//        var regionDomain = await regionRepository.GetAllAsync();

//        //Map the RegionsDomain to RegionDto

//        var regionDto = new List<RegionDto>();

//        foreach (var region in regionDomain)
//        {
//            regionDto.Add(new RegionDto()
//            {
//                Id = region.Id,
//                Code = region.Code,
//                Name = region.Name,
//                RegionImageUrl = region.RegionImageUrl
//            });
//        }

//        return Ok(regionDto);
//    }

//    //Get Region by ID
//    [HttpGet]
//    [Route("{id:guid}")]
//    public async Task<IActionResult> GetById([FromRoute] Guid id)
//    {
//        var region = await regionRepository.GetByIdAsync(id);

//        if (region == null)
//        {
//            return NotFound();
//        }

//        //Map Region Domain to region dto
//        var regionDto = new RegionDto
//        {
//            Id = region.Id,
//            Code = region.Code,
//            Name = region.Name,
//            RegionImageUrl = region.RegionImageUrl
//        };

//        return Ok(regionDto);

//        //return Ok(region);
//    }

//    //Create Region
//    [HttpPost]
//    public async Task<IActionResult> create([FromBody] CreateRegionRequestDto createRegion)
//    {
//        //Map the CreateRegionRequestDto to Region Domain model
//        var regionDomain = new Region
//        {
//            Code = createRegion.Code,
//            Name = createRegion.Name,
//            RegionImageUrl = createRegion.RegionImageUrl
//        };

//        //create the region in DB using domain model
//        regionDomain = await regionRepository.CreateAsync(regionDomain);

//        //Map the region domain model to region dto for user output

//        var regionDto = new RegionDto
//        {
//            Id = regionDomain.Id,
//            Code = regionDomain.Code,
//            Name = regionDomain.Name,
//            RegionImageUrl = regionDomain.RegionImageUrl
//        };

//        return Ok(regionDto);

//        //return the createAtAction which will send 201 response to the client
//        //return CreatedAtAction(nameof(GetById), new { Id = regionDto.Id }, regionDto);
//    }

//    //Update the Region
//    [HttpPut]
//    [Route("{id:guid}")]
//    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateregion)
//    {
//        var regionDomain = new Region()
//        {
//            Code = updateregion.Code,
//            Name = updateregion.Name,
//            RegionImageUrl = updateregion.RegionImageUrl
//        };

//        regionDomain = await regionRepository.UpdateAsync(id, regionDomain);

//        if (regionDomain == null)
//        {
//            return NotFound();
//        }

//        //map the region domain model to  region dto
//        var regionDto = new RegionDto
//        {
//            Id = regionDomain.Id,
//            Code = regionDomain.Code,
//            Name = regionDomain.Name,
//            RegionImageUrl = regionDomain.RegionImageUrl
//        };

//        return Ok(regionDto);
//    }

//    //Delete the Region
//    [HttpDelete]
//    [Route("{id:guid}")]
//    public async Task<IActionResult> Delete([FromRoute] Guid id)
//    {
//        var regionDomain = await regionRepository.DeleteAsync(id);

//        if (regionDomain == null)
//        {
//            return NotFound();
//        }

//        return Ok($"{regionDomain.Name}  Region Deleted Successfully ");
//    }


//}










//Without Repository

//public class RegionsController : ControllerBase
//{
//    private readonly NZWalksDbContext dbContext;
//    public RegionsController(NZWalksDbContext dbcontext)
//    {
//        this.dbContext = dbcontext;
//    }

//    //Get All Regions
//    [HttpGet]
//    public IActionResult GetAll()
//    {
//        //Normal method
//        /*
//        var region = dbContext.Regions.ToList();
//        return Ok(region);
//        */

//        //With DTo
//        var regionDomain = dbContext.Regions.ToList();

//        //Map the RegionsDomain to RegionDto

//        var regionDto = new List<RegionDto>();

//        foreach (var region in regionDomain)
//        {
//            regionDto.Add(new RegionDto()
//            {
//                Id = region.Id,
//                Code = region.Code,
//                Name = region.Name,
//                RegionImageUrl = region.RegionImageUrl
//            });
//        }

//        return Ok(regionDto);
//    }

//    //Get Region by ID
//    [HttpGet]
//    [Route("{id:guid}")]
//    public IActionResult GetById([FromRoute] Guid id)
//    {
//        var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);

//        if (region == null)
//        {
//            return NotFound();
//        }

//        //Map Region Domain to region dto
//        var regionDto = new RegionDto
//        {
//            Id = region.Id,
//            Code = region.Code,
//            Name = region.Name,
//            RegionImageUrl = region.RegionImageUrl
//        };

//        return Ok(regionDto);

//        //return Ok(region);
//    }

//    //Create Region
//    [HttpPost]
//    public IActionResult create([FromBody] CreateRegionRequestDto createRegion)
//    {
//        //Map the CreateRegionRequestDto to Region Domain model
//        var regionDomain = new Region
//        {
//            Code = createRegion.Code,
//            Name = createRegion.Name,
//            RegionImageUrl = createRegion.RegionImageUrl
//        };

//        //create the region in DB using domain model
//        dbContext.Regions.Add(regionDomain);
//        dbContext.SaveChanges();

//        //Map the region domain model to region dto for user output

//        var regionDto = new RegionDto
//        {
//            Id = regionDomain.Id,
//            Code = regionDomain.Code,
//            Name = regionDomain.Name,
//            RegionImageUrl = regionDomain.RegionImageUrl
//        };

//        //return Ok(regionDto);

//        //return the createAtAction which will send 201 response to the client
//        return CreatedAtAction(nameof(GetById), new { Id = regionDto.Id }, regionDto);
//    }

//    //Update the Region
//    [HttpPut]
//    [Route("{id:guid}")]
//    public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateregion)
//    {
//        var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id == id);

//        if (regionDomain == null)
//        {
//            return NotFound();
//        }

//        //Map the updateRegionDto to region domain model
//        regionDomain.Code = updateregion.Code;
//        regionDomain.Name = updateregion.Name;
//        regionDomain.RegionImageUrl = updateregion.RegionImageUrl;

//        //Update the chages to DB
//        dbContext.SaveChanges();

//        //map the region domain model to  region dto
//        var regionDto = new RegionDto
//        {
//            Id = regionDomain.Id,
//            Code = regionDomain.Code,
//            Name = regionDomain.Name,
//            RegionImageUrl = regionDomain.RegionImageUrl
//        };

//        return Ok(regionDto);
//    }

//    //Delete the Region
//    [HttpDelete]
//    [Route("{id:guid}")]
//    public IActionResult Delete([FromRoute] Guid id)
//    {
//        var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id == id);

//        if (regionDomain == null)
//        {
//            return NotFound();
//        }

//        //Remove the region in db
//        dbContext.Regions.Remove(regionDomain);
//        dbContext.SaveChanges();

//        return Ok("Region Deleted Successfully ");
//    }


//}




//This is the dummy method
/*
[HttpGet]
public IActionResult Dummy()
{
    var region = new List<Region>
    {
        new Region
        {
            Id = Guid.NewGuid(),
            Code = "AKL",
            Name = "Auckland",
            RegionImageUrl = "img.jpg"
        }
     };
    return Ok(region);
}
*/