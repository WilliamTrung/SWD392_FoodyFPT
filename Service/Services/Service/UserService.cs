using ApplicationCore.Context;
using ApplicationCore.Models;
using AutoMapper;
using Service.Service;
using Service.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Service
{
    public class UserService : BaseService<DTO.User, User>, IUserService
    {
        private FoodyContext _context;
        private IMapper _mapper;
        public UserService(IMapper mapper, FoodyContext context) : base(mapper, context)
        {
            _context = context;
            _mapper = mapper;
        }
        //trungnt 10-10-2022 function 
        //parameter: email - login email
        //flow:
        //find user by email
        //if found -> return
        //if not found -> register auto -> return
        public async Task<DTO.User> LoginAsync(DTO.User loginUser, string? roleSet = null)
        {
            //throw new NotImplementedException();
            var find = await GetAsync(filter: u => u.Email == loginUser.Email);
            var found = find == null ? null : find.FirstOrDefault();
            if(found == null)
            {
                //not in db
                //set default value for new user
                loginUser.Phone = "No data";
                if(roleSet != null && roleSet != "Administrator")
                {
                    var roleFind = _context.Roles.Where(r => r.Name == roleSet).FirstOrDefault();
                    if(roleFind != null)
                    {
                        loginUser.RoleId = roleFind.Id;
                    } else
                    {
                        throw new Exception("Role not found!");
                    }
                } else
                {
                    loginUser.RoleId = 2;
                }
                found = await CreateAsync(loginUser);
            }
            var role =  _context.Roles.FirstOrDefault(r => r.Id == found.RoleId);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            role.Users = null;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            found.Role = _mapper.Map<DTO.Role>(role);
            return found;
        }
    }
}
