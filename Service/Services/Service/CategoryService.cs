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
    public class CategoryService : BaseService<DTO.Category, Category>, ICategoryService
    {
        IMapper _mapper;
        IGenericRepository<Category> _repository;
        public CategoryService(IMapper mapper, FoodyContext context) : base(mapper, context)
        {
            _mapper = mapper;
            _repository = new GenericRepository<Category>(context);
        }
    }
}
