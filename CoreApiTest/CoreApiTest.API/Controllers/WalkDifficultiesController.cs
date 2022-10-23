using AutoMapper;
using CoreApiTest.API.Data;
using CoreApiTest.API.Models.DTO;
using CoreApiTest.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoreApiTest.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultiesController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultiesController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficultiesAsync()
        {
            var walkDifficultiesDomain = await walkDifficultyRepository.GetAllAsync();
            var walkDifficultyDTO = mapper.Map<List<Models.DTO.WalkDifficulty>>(walkDifficultiesDomain);
            return Ok(walkDifficultyDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultyById")]
        public async Task<IActionResult> GetWalkDifficultyById(Guid id)
        {
            var walkDifficluty = await walkDifficultyRepository.GetAsync(id);
            if (walkDifficluty == null)
                return NotFound();

            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficluty);
            return Ok(walkDifficultyDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkDifficultyAsync(Models.DTO.AddDifficultyRequest addDifficultyRequest)
        {
            // Convert DTO to Domain model
            var walkDifficultyDomain = new Models.Domain.WalkDifficulty
            {
                Code = addDifficultyRequest.Code
            };

            // Call repository
            walkDifficultyDomain = await walkDifficultyRepository.AddAsync(walkDifficultyDomain);

            // Convert to DTO
            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficultyDomain);

            return CreatedAtAction(nameof(GetWalkDifficultyById), new { id = walkDifficultyDTO.Id }, walkDifficultyDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateDifficultyAsync(Guid id, Models.DTO.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            var walkDifficlutyDomain = new Models.Domain.WalkDifficulty
            {
                Code = updateWalkDifficultyRequest.Code
            };

            walkDifficlutyDomain = await walkDifficultyRepository.UpdateAsync(id, walkDifficlutyDomain);

            if (walkDifficlutyDomain == null)
                return NotFound();

            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficlutyDomain);

            return Ok(walkDifficultyDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkDifficultyAsync(Guid id)
        {
            var exisitngWalkDifficulty = await walkDifficultyRepository.DeleteAsync(id);
            if (exisitngWalkDifficulty == null)
                return NotFound();

            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(exisitngWalkDifficulty);

            return Ok(walkDifficultyDTO);
        }
    }
}
