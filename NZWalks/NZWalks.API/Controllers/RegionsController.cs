using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")] // Regions Endpoint
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

            /*
            var response = new List<RegionDTO>();

            regions.ToList().ForEach(domain =>
            {
                var dto = new RegionDTO()
                {
                    RegionId = domain.Id,
                    Code = domain.Code,
                    Name = domain.Name,
                    Area = domain.Area,
                    Lat = domain.Lat,
                    Long = domain.Long,
                    Population = domain.Population,
                };
                response.Add(dto);
            });
            */

            var response = mapper.Map<List<RegionDTO>>(regions);

            return Ok(response); 
        }

        
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetRegionsAsync(Guid id)
        {
            var region = await regionRepository.GetAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            var response = mapper.Map<RegionDTO>(region);

            return Ok(response);
        }
        
    }
}
