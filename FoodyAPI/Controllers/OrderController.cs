using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.Services.IService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodyAPI.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;
        private IOrderDetailService _orderDetailService;
        public OrderController(IOrderService orderService, IOrderDetailService orderDetailService)
        {
            _orderService = orderService;
            _orderDetailService = orderDetailService;
        }
        // GET: api/<OrderController>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var list = await _orderService.GetAsync();
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var dto = await _orderService.GetAsync(filter: order => order.Id == id);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        // POST api/<OrderController>
        [HttpPost]
        public async Task<IActionResult> PostAsync(Order order)
        {
            try
            {
                var create = await _orderService.CreateAsync(order);
                if (create != null)
                {
                    return Ok(create);
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

        // POST api/<OrderController>
        [HttpPost("checkout")]
        public async Task<IActionResult> CheckOut(Order order)
        {
            try
            {
                var result = await _orderService.CheckOut(order);
                if (result)
                {
                    return Ok(StatusCodes.Status200OK);
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


        // PUT api/<OrderController>/5
        [HttpPut]
        public async Task<IActionResult> Put(Order order)
        {
            try
            {
                var updated = await _orderService.UpdateAsync(order.Id, order);
                var updated_menudetail = _orderDetailService.UpdateDetailsAsync(order.OrderDetails);
                if (updated != null && updated_menudetail.Result == true)
                {
                    return Ok(StatusCodes.Status200OK);
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

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return BadRequest(StatusCodes.Status501NotImplemented);
        }
    }
}
