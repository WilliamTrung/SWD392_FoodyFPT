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
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        // GET: api/<CategoryController>
        ICategoryService _categoryService;
        public CategoryController(FoodyContext context, IMapper mapper)
        {
            _categoryService = new CategoryService(mapper, context);
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var list = await _categoryService.GetAsync();
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var dto = await _categoryService.GetByIdAsync(id);
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
            var list = await _categoryService.GetAsync(p => p.Name.ToUpper().Contains(name.ToUpper()));
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Category category)
        {
            try
            {
                var created = await _categoryService.CreateAsync(category);
                return Ok(created);
            }
            catch
            {
                return BadRequest(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{category}")]
        public async Task<IActionResult> PutAsync([FromQuery] Category category)
        {
            try
            {
                var updated = await _categoryService.UpdateAsync(0,category);
                return Ok(updated);
            }
            catch
            {
                return BadRequest(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _categoryService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
