using ApplicationCore.Context;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.Services.IService;
using Service.Services.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodyAPI.Controllers
{
    [Route("api/menu")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private IMenuService _menuService;
        private IMenuDetailService _menuDetailService;
        // GET: api/<LocationController>
        /*
        public MenuController(FoodyContext context, IMapper mapper)
        {
            _menuService = new MenuService(mapper, context);
            _menuDetailService = new MenuDetailService(mapper, context);
        }
        */
        public MenuController(IMenuService menuService, IMenuDetailService menuDetailService)
        {
            _menuService = menuService;
            _menuDetailService = menuDetailService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var list = await _menuService.GetAsync(includeProperties: "MenuDetails");
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
            var dto = await _menuService.GetAsync(filter: menu => menu.Id == id);
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
            var list = await _menuService.GetAsync(filter: p => p.Name.ToUpper().Contains(name.ToUpper()));
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }
        // POST api/<LocationController>
        // POST api/<CategoryController>
        [HttpPost]
        public async Task<IActionResult> PostAsync(Menu menu)
        {
            try
            {
                /*
                var menu = new Menu()
                {
                    Name = name
                };
                */
                var create = await _menuService.CreateAsync(menu);
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

        // PUT api/<CategoryController>/5
        [HttpPut]
        public async Task<IActionResult> PutAsync(Menu menu)
        {
            try
            {
                /*
                var menu = new Menu()
                {
                    Id = id,
                    Name = name,
                    Status = true
                };
                */  
                var details = _menuDetailService.Distinct(menu.Id, menu.MenuDetails);
                if(details != null && details.Result != null)
                {
                    menu.MenuDetails = details.Result.ToList();
                } 
                //update menu name
                var updated = await _menuService.UpdateAsync(menu.Id, menu);
                //update menu detail
                var updated_menudetail = _menuDetailService.UpdateDetailsAsync(menu.MenuDetails);
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
        //PUT disable/enable 
        //change entity's status
        [HttpPut("enable-disable/{id}")]
        public async Task<IActionResult> SwitchStatusAsync(int id)
        {
            try
            {
                var menu = await _menuService.GetByIdAsync(id);
                if (menu.Status == false)
                {
                    menu.Status = true;
                }
                else
                {
                    menu.Status = false;
                }
                var updated = await _menuService.UpdateAsync(id, menu);
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
            //bool result = await _menuService.DeleteAsync(id);
            //return Ok(result);
            return BadRequest(StatusCodes.Status501NotImplemented);//Ok("Not yet implemented!");
        }
    }
}
