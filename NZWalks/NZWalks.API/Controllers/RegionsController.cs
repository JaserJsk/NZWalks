using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Models.Requests;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")] // Regions Endpoint
    public class RegionsController : Controller
    {
        #region Fields
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        #endregion

        #region Constructor
        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        #endregion

        #region GET ALL
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
        #endregion

        #region GET
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionsAsync")]
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
        #endregion

        #region ADD
        [HttpPost]
        public async Task<IActionResult> AddRegionsAsync(AddRegionRequest request)
        {
            // Converting the request into domain model
            var newItem = new Region()
            {
                Code = request.Code,
                Name = request.Name,
                Area = request.Area,
                Lat = request.Lat,
                Long = request.Long,
                Population = request.Population,
            };

            var region = await regionRepository.AddAsync(newItem);

            var response = mapper.Map<RegionDTO>(region);

            return CreatedAtAction(nameof(GetRegionsAsync), new {id = response.RegionId}, response);
        }
        #endregion

        #region UPDATE
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync(
            [FromRoute] Guid id, [FromBody] UpdateRegionRequest request)
        {
            // Converting the request into domain model
            var updateItem = new Region()
            {
                Code = request.Code,
                Name = request.Name,
                Area = request.Area,
                Lat = request.Lat,
                Long = request.Long,
                Population = request.Population,
            };

            var region = await regionRepository.UpdateAsync(id, updateItem);

            if (region == null)
            {
                return NotFound();
            }

            var response = mapper.Map<RegionDTO>(region);

            return Ok(response);
        }
        #endregion

        #region DELETE
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionsAsync(Guid id)
        {
            var region = await regionRepository.DeleteAsync(id);

            if (region == null)
            { 
                return NotFound();
            }

            var response = mapper.Map<RegionDTO>(region);

            return Ok(response);
        }
        #endregion

    }
}
