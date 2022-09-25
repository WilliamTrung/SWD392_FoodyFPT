using ApplicationCore.Context;
using ApplicationCore.Models;
using ApplicationCore.Repository;
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
    public class MenuService : BaseService<DTO.Menu, Menu>, IMenuService
    {
        IMapper _mapper;
        IGenericRepository<Menu> _repository;

        public MenuService(IMapper mapper, FoodyContext context) : base(mapper, context)
        {
            _mapper = mapper;
            _repository = new GenericRepository<Menu>(context);
        }

    }
}
