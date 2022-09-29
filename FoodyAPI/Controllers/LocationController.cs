using ApplicationCore.Context;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.Services.IService;
using Service.Services.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodyAPI.Controllers
{
    [Route("api/location")]
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
        public async Task<IActionResult> PostAsync(string name, int floor)
        {
            try
            {
                var location = new Location()
                {
                    Name = name,
                    Floor = floor
                };
                var created = await _locationService.CreateAsync(location);
                if (created != null)
                {
                    return Ok(created);
                }
                else
                {
                    return BadRequest(StatusCodes.Status500InternalServerError);
                }
            }
            catch
            {
                return BadRequest(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT api/<CategoryController>/5
        [HttpPut]
        public async Task<IActionResult> PutAsync(int id, string name, int floor)
        {
            try
            {
                var location = new Location()
                {
                    Id = id,
                    Name = name,
                    Floor = floor,
                    Status = true
                };
                var updated = await _locationService.UpdateAsync(id, location);
                if (updated != null)
                {
                    return Ok(updated);
                }
                else
                {
                    return BadRequest(StatusCodes.Status500InternalServerError);
                }
            }
            catch
            {
                return BadRequest(StatusCodes.Status500InternalServerError);
            }
        }
        //PUT disable/enable 
        //change entity's status
        [HttpPut("enable-disable/{id}")]
        public async Task<IActionResult> SwitchStatusAsync(int id)
        {
            try
            {
                var location = await _locationService.GetByIdAsync(id);
                if(location.Status == null || location.Status == false)
                {
                    location.Status = true;
                } else
                {
                    location.Status = false;
                }
                var updated = await _locationService.UpdateAsync(id, location);
                return Ok(updated);
            } catch
            {
                return BadRequest(StatusCodes.Status500InternalServerError);
            }
        }


        // DELETE api/<LocationController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //bool result = await _locationService.DeleteAsync(id);
            //return Ok(result);
            return Ok("Not yet implemented!");
        }
    }
}
