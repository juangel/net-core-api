using AutoMapper;
using CoreApiTest.API.Models.Domain;
using CoreApiTest.API.Models.DTO;
using CoreApiTest.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoreApiTest.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        

        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            var regions = await regionRepository.GetAllAsync();

            //var regionsDTO = new List<Models.DTO.Region>();

            //regions.ToList().ForEach(region =>
            //{
            //    var regionDTO = new Models.DTO.Region()
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        Area = region.Area,
            //        Code = region.Code,
            //        Lat = region.Lat,
            //        Long = region.Long,
            //        Population = region.Population
            //    };
            //});

            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);

            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionRepository.GetAsync(id);
            
            if(region == null)
            {
                return NotFound();
            }

            var regionDTO = mapper.Map<Models.DTO.Region>(region);

            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(AddRegionRequest addRegionRequest)
        {
            // Request (DTO) to Domain Model
            var region = new Models.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Name = addRegionRequest.Name,
                Population = addRegionRequest.Population
            };

            // Pass details to repository
            region = await regionRepository.AddAsync(region);

            // Convert data back to DTO
            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };

            // Regresamos objeto creado, pero usamos otro metodo para regresar. 
            // Regresamos usando el metodo GetRegionAsync mandando como parametro el id registrado y por ultimo el objeto creado.
            // Al hacerlo de esta manera se regresa un "location"  en la respuesta, que es la direccion en donde pueden consultar el objeto creado
            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id }, regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            // Delete region from db
            var region = await regionRepository.DeleteAsync(id);

            // If null NotFound
            if (region == null)
                return NotFound();

            // Convert response back to DTO
            var regionDTO = mapper.Map<Models.DTO.Region>(region);

            // return Ok Response
            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] UpdateRegionRequest updateRegionRequest)
        {
            // Convert DTO to domain model
            var domainRegion = new Models.Domain.Region()
            {
                Name = updateRegionRequest.Name,
                Area = updateRegionRequest.Area,
                Code = updateRegionRequest.Code,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Population = updateRegionRequest.Population
            };

            // update the region using repository
            domainRegion = await regionRepository.UpdateAsync(id, domainRegion);

            // if null NotFound()
            if (domainRegion == null)
                return NotFound();

            // Convert Domain back to DTO
            var regionDTO = mapper.Map<Models.DTO.Region>(domainRegion);

            //Return Ok
            return Ok(regionDTO);
        }
    }
}
