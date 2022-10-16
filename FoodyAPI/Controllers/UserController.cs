using ApplicationCore.Context;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.Services.IService;
using Service.Services.Service;

namespace FoodyAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        /*
        public UserController(FoodyContext context, IMapper mapper)
        {
            _userService = new UserService(mapper, context);
        }
        */
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list_user = await _userService.GetAsync();
            if (list_user != null && list_user.Count() > 0)
            {
                return Ok(list_user);
            }
            return NotFound();
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var list_user = await _userService.GetAsync(filter: user => user.Name.ToUpper().Contains(name.ToUpper()));
            if (list_user != null && list_user.Count() > 0)
            {
                return Ok(list_user);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post(User user)//int roleId, string name, string email, string phone, string image)
        {
            try
            {
                /*
                var user = new User()
                {
                    Name = name,
                    Email = email,
                    RoleId = roleId,
                    Phone = phone,
                    Image = image
                };
                */
                var created = await _userService.CreateAsync(user);
                if(created != null)
                {
                    return Ok(created);
                } else
                {
                    return BadRequest(StatusCodes.Status500InternalServerError);
                }
            }
            catch
            {
                return BadRequest(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Put(User user)
        {
            try
            {
                /*
                var user = new User()
                {
                    Id = userId,
                    Name = name,
                    Email = email,
                    RoleId = roleId,
                    Phone = phone,
                    Image = image
                };
                */
                var updated = await _userService.UpdateAsync(user.Id, user);
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
        [HttpDelete]
        public async Task<IActionResult> Delete(int userId)
        {
            return BadRequest(StatusCodes.Status501NotImplemented);//Ok("Not yet implemented!");
        }
    }
}
