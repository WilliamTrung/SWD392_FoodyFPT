using FoodyAPI.Filter;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.Services.IService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodyAPI.Controllers
{
    [CustomActionFilter]
    [Route("api/shipper")]
    [ApiController]
    public class ShipperController : ControllerBase
    {
        IShipperService _shipperService;
        IUserService _userService;
        public ShipperController(IShipperService shipperService)
        {
            _shipperService = shipperService;
        }
        // GET: api/<ShipperController>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var list = await _shipperService.GetAsync();
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }

        // GET api/<ShipperController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var dto = await _shipperService.GetByIdAsync(id);
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
            var list = await _shipperService.GetAsync(p => p.User.Name.ToUpper().Contains(name.ToUpper()));   
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }

        // POST api/<ShipperController>
        [HttpPost]
        public async Task<IActionResult> PostAsync(Shipper shipper)
        {
            try
            {
                var created = await _shipperService.CreateAsync(shipper);
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

        // PUT api/<ShipperController>/5
        [HttpPut]
        public async Task<IActionResult> PutAsync(Shipper shipper)
        {
            try
            {
                var update = await _shipperService.UpdateAsync(shipper.Id, shipper);
                if (update != null)
                {
                    return Ok(update);
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

        // DELETE api/<ShipperController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return BadRequest(StatusCodes.Status501NotImplemented);
        }
    }
}
