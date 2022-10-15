using ApplicationCore.Context;
using ApplicationCore.Models;
using ApplicationCore.Repository;
using AutoMapper;
using Service.Service;
using Service.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Service
{
    public class ShipperService : BaseService<DTO.Shipper, Shipper>, IShipperService
    {
        IMapper _mapper;
        IGenericRepository<Shipper> _repository;
        IUserService _userService;
        FoodyContext _foodContext;
        public ShipperService(IMapper mapper, FoodyContext context) : base(mapper, context)
        {
            _mapper = mapper;
            _foodContext = context;
            _repository = new GenericRepository<Shipper>(context);
            _userService = new UserService(mapper, context);
        }

        public override Task<IEnumerable<DTO.Shipper>> GetAsync(Expression<Func<Shipper, bool>>? filter = null)
        {
            var list = _foodContext.Shippers.ToList();
            foreach(var item in list)
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                User user = _foodContext.Users.FirstOrDefault(x => x.Id == item.UserId);
                if(user != null)
                {
                    user.Shippers = null;
                    item.User = user;
                }
            }
            if(filter != null)
            {
                list = list.AsQueryable().Where(filter).ToList();
            }
            var list_dto = new List<DTO.Shipper>();
            foreach(var item in list)
            {
                list_dto.Add(_mapper.Map<DTO.Shipper>(item));
            }
            return Task.FromResult(list_dto.AsEnumerable());
        }
    }
}
