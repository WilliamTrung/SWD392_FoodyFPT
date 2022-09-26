using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using Service.DTO;
using ApplicationCore.Context;
using System;
using Service.Services.IService;
using Service.Services.Service;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        
        IStoreService _storeService;
        public StoreController(FoodyContext context, IMapper mapper)
        {
            _storeService = new StoreService(mapper,context);
        }
        // GET: api/<StoreController>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var list = await _storeService.GetAsync();
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }

        // GET api/<StoreController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var dto = await _storeService.GetByIdAsync(id);
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
            var list = await _storeService.GetAsync(p => p.Name.ToUpper().Contains(name.ToUpper()));
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }

        // POST api/<StoreController>
        [HttpPost]
        public async Task<IActionResult> PostAsync(string name, string address, string phone, string email)
        {
            try
            {
                var store = new Store()
                {
                    Name = name,
                    Address = address,
                    Phone = phone,
                    Email = email
                };
                var create = await _storeService.CreateAsync(store);
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

        // PUT api/<StoreController>/5
        [HttpPut("{store}")]
        public async Task<IActionResult> PutAsync(int id, string name, string address, string phone, string email)
        {
            try
            {
                var store = new Store()
                {
                    Id = id,
                    Name = name,
                    Address = address,
                    Phone = phone,
                    Email = email
                };
                var updated = await _storeService.UpdateAsync(id, store);
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

        // DELETE api/<StoreController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _storeService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
