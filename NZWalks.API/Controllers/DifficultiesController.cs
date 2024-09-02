using AutoMapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using NZWalks.API.Data;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultiesController : ControllerBase
    {
        private readonly IDifficultyRepository difficultyRepository;

        private readonly IMapper mapper;
        public DifficultiesController(IDifficultyRepository repository, IMapper map)
        {
            this.difficultyRepository = repository; 
            this.mapper = map;
        }

        //Get all difficulties from database
        [HttpGet]
        public async Task<IActionResult> GetAllDifficulty()
        {
            //Get data from db
            var difficulty = await difficultyRepository.GetAllDifficultyAsync();

            //Map data to dto
            var difficultyDto = mapper.Map<List<DifficultyDto>> (difficulty);

            return Ok(difficultyDto);
        }
    }
}
