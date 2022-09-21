using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using Service.DTO;
using ApplicationCore.Context;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // GET: api/<ProductController>
        IProductService _productService;
        public ProductController(FoodyContext context, IMapper mapper)
        {
            _productService = new ProductService(mapper,context);
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var list = await _productService.GetAsync();
            if(list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var dto = await _productService.GetByIdAsync(id);
            if(dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Product _product)
        {
            try
            {
                var created = await _productService.CreateAsync(_product);
                return Ok(created);
            }
            catch
            {
                return BadRequest(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT api/<ProductController>/5
        [HttpPut("{_product}")]
        public async Task<IActionResult> Put([FromBody] Product _product)
        {
            try
            {
                var updated = await _productService.UpdateAsync(_product);
                return Ok(updated);
            }
            catch
            {
                return BadRequest(StatusCodes.Status500InternalServerError);
            }
        }
        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromBody] int _id)
        {
            bool result = await _productService.DeleteAsync(_id);
            return Ok(result);
        }
    }
}
