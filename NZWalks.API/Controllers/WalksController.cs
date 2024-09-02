using AutoMapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkrepository, IMapper map)
        {
            this.walkRepository = walkrepository;
            this.mapper = map;
        }


        //Create the walk

        [HttpPost]
        public async Task<IActionResult> create(CreateWalkRequestDto createWalk)
        {
            //Map the wlakDto to the Domain model
            var walkDomain = mapper.Map<Walk>(createWalk);

            //Create the wal in db 
            walkDomain = await walkRepository.CreateAsync(walkDomain);

            //Mpa the walk domain back to walkdto

            var walkDto = mapper.Map<CreateWalkRequestDto>(walkDomain);


            return Ok(walkDto);

        }

        //Get All walk details
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get walk data from Db 
            var walkDomain = await walkRepository.GetAllAsync();

            //Map the walk data to walk dto
            var walkDto = mapper.Map<List<WalkDto>>(walkDomain);

            return Ok(walkDto);
        }

        //Get walk by Id
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //Get data from db
            var walkDomain = await walkRepository.GetByIdAsync(id);

            //Map domain model to dto
            var walkDto = mapper.Map<WalkDto>(walkDomain);

            return Ok(walkDto);

        }

        //Update the walk
        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody]UpdateWalkRequestDto updateWalk)
        {
            //Map Dto to domain model
            var walkDomain = mapper.Map<Walk>(updateWalk);

            //Update data in database
            walkDomain = await walkRepository.UpdateAsync(id, walkDomain);

            if(walkDomain == null)
            {
                return NotFound();
            }

            //Map walk domain to dto
            var walkDto = mapper.Map<WalkDto>(walkDomain);

            return Ok(walkDto);
        }

        //Delete the walk
        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            var walkDomain =  await walkRepository.DeleteAsync(id);

            if (walkDomain == null)
            {
                return NotFound();
            }

            return Ok($"{walkDomain.Name} deleted successfully");
        }

    }
}
