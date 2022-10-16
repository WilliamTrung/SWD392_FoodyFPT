using ApplicationCore.Context;
using ApplicationCore.Models;
using ApplicationCore.Repository;
using AutoMapper;
using Service.Helper;
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
    public class MenuService : BaseService<DTO.Menu, Menu>, IMenuService
    {
        IMapper _mapper;
        IGenericRepository<Menu> _repository;
        IMenuDetailService _menuDetailService;

        public MenuService(IMapper mapper, FoodyContext context) : base(mapper, context)
        {
            _mapper = mapper;
            _repository = new GenericRepository<Menu>(context);
            _menuDetailService = new MenuDetailService(_mapper, context);
        }

        public override Task<IEnumerable<DTO.Menu>> GetAsync(PagingRequest? paging = null, Expression<Func<Menu, bool>>? filter = null, string? includeProperties = null)
        {
            var result =  base.GetAsync(paging, filter, includeProperties);
            foreach (var item in result.Result)
            {
                var details = _menuDetailService.GetAsync(filter: detail => detail.MenuId == item.Id).Result.ToList();
                if(details != null)
                {
                    item.MenuDetails = details;
                }
                
            }
            return result;
        }
    }
}
