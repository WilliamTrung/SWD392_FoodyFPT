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
    }
}
