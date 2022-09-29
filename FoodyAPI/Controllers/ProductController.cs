using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using Service.DTO;
using ApplicationCore.Context;
using System;
using Service.Services.IService;
using Service.Services.Service;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodyAPI.Controllers
{
    [Route("api/product")]
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
        // GET api/<ProductController>/name
        [HttpGet("name/{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var list = await _productService.GetAsync(p => p.Name.ToUpper().Contains(name.ToUpper()));
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }
        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> PostAsync(int categoryId, int storeId, string name, decimal price, int quantity)
        {
            try
            {
                var product = new Product()
                {
                    CategoryId = categoryId,
                    StoreId = storeId,
                    Name = name,
                    Price = price,
                    Quantity = quantity
                };
                var create = await _productService.CreateAsync(product);
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

        // PUT api/<ProductController>/5
        [HttpPut("{product}")]
        public async Task<IActionResult> PutAsync(int id, int categoryId, int storeId, string name, decimal price, int quantity)
        {
            try
            {
                var product = new Product()
                {
                    Id = id,
                    CategoryId = categoryId,
                    StoreId = storeId,
                    Name = name,
                    Price = price,
                    Quantity = quantity
                };
                var updated = await _productService.UpdateAsync(id, product);
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
        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //bool result = await _productService.DeleteAsync(id);
            //return Ok(result);
            return Ok("Not yet implemented!");
        }
    }
}
