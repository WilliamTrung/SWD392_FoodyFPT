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
    public class ProductService : BaseService<DTO.Product, Product>, IProductService
    {
        IMapper _mapper;
        IGenericRepository<Product> _repository;

        public ProductService(IMapper mapper, FoodyContext context) : base(mapper, context)
        {
            _mapper = mapper;
            _repository = new GenericRepository<Product>(context);
        }
        public override Product DisableSelfReference(Product entity)
        {
            //base.DisableSelfReference(entity);
            if (entity.Category != null)
            {
                entity.Category.Products = null;
            }
            if (entity.Store != null)
            {
                entity.Store.Products = null;
            }
            return entity;
        }
        public override Task<IEnumerable<DTO.Product>> GetAsync(PagingRequest? paging = null, Expression<Func<Product, bool>>? filter = null, string? includeProperties = null)
        {
            var list = base.GetAsync(paging, filter, includeProperties);
            // set property self reference to null
            /*
            foreach(var item in list.Result)
            {
                DisableSelfReference(_mapper.Map<Product>(item));
            }
            */
            return list;
        }
    }
}
