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
    [Route("api/store")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        
        IStoreService _storeService;
        /*
        public StoreController(FoodyContext context, IMapper mapper)
        {
            _storeService = new StoreService(mapper,context);
        }
        */
        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
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
            var list = await _storeService.GetAsync(filter: p => p.Name.ToUpper().Contains(name.ToUpper()));
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }

        // POST api/<StoreController>
        [HttpPost]
        public async Task<IActionResult> PostAsync(Store store)
        {
            try
            {
                /*
                var store = new Store()
                {
                    Name = name,
                    Address = address,
                    Phone = phone,
                    Email = email
                };
                */
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
        [HttpPut]
        public async Task<IActionResult> PutAsync(Store store)
        {
            try
            {
                /*
                var store = new Store()
                {
                    Id = id,
                    Name = name,
                    Address = address,
                    Phone = phone,
                    Email = email,
                    Status = true
                };
                */
                var updated = await _storeService.UpdateAsync(store.Id, store);
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
                var store = await _storeService.GetByIdAsync(id);
                if (store.Status == null || store.Status == false)
                {
                    store.Status = true;
                }
                else
                {
                    store.Status = false;
                }
                var updated = await _storeService.UpdateAsync(id, store);
                return Ok(updated);
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
            return BadRequest(StatusCodes.Status501NotImplemented);//Ok("Not yet implemented!");
            /*
            bool result = await _storeService.DeleteAsync(id);
            return Ok(result);
            */
        }
    }
}
