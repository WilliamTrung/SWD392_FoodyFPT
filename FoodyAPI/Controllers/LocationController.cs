using ApplicationCore.Context;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.Services.IService;
using Service.Services.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private ILocationService _locationService;
        // GET: api/<LocationController>
        public LocationController(FoodyContext context, IMapper mapper)
        {
            _locationService = new LocationService(mapper, context);
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var list = await _locationService.GetAsync();
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }

        // GET api/<LocationController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var dto = await _locationService.GetByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }
        // GET api/<ProductController>/name
        [HttpGet("name/{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var list = await _locationService.GetAsync(p => p.Name.ToUpper().Contains(name.ToUpper()));
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }
        // POST api/<LocationController>
        // POST api/<CategoryController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Location location)
        {
            try
            {
                var created = await _locationService.CreateAsync(location);
                return Ok(created);
            }
            catch
            {
                return BadRequest(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{location}")]
        public async Task<IActionResult> PutAsync([FromQuery] Location location)
        {
            try
            {
                var updated = await _locationService.UpdateAsync(location);
                return Ok(updated);
            }
            catch
            {
                return BadRequest(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE api/<LocationController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _locationService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
