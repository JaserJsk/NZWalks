using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
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
        public async Task<IActionResult> GetAllRegions() {

            var regions = await regionRepository.GetAllAsync();

            /*
            var regionsDTO = new List<RegionDTO>();

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
                regionsDTO.Add(dto);
            });
            */

            var regionsDTO = mapper.Map<List<RegionDTO>>(regions);

            return Ok(regionsDTO); 
        }
    }
}
