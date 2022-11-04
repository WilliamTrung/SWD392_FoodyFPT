using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using Service.DTO;
using ApplicationCore.Context;
using System;
using Service.Services.IService;
using Service.Services.Service;
using FoodyAPI.Filter;
using Service.Helper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlotController : ControllerBase
    {
        ISlotService _slotService;

        public SlotController(ISlotService slotService)
        {
            _slotService = slotService;
        }

        // GET: api/<SlotController>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var list = await _slotService.GetAsync(includeProperties: "Menu");
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }

        // GET api/<SlotController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var dto = await _slotService.GetAsync(filter: s => s.Id == id, includeProperties: "Menu");
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        // POST api/<SlotController>
        [HttpPost]
        public async Task<IActionResult> PostAsync(Slot slot)
        {
            try
            {
                var created = await _slotService.CreateAsync(slot);
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

        // PUT api/<SlotController>/5
        [HttpPut]
        public async Task<IActionResult> PutAsync(Slot slot)
        {
            try
            {
                var update = await _slotService.UpdateAsync(slot.Id, slot);
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

        // DELETE api/<SlotController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return BadRequest(StatusCodes.Status501NotImplemented);
        }
    }
}
