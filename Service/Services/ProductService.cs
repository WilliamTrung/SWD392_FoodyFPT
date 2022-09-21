using ApplicationCore.Context;
using ApplicationCore.Models;
using ApplicationCore.Repository;
using AutoMapper;
using Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ProductService : BaseService<DTO.Product,Product>, IProductService
    {
        IMapper _mapper;
        IGenericRepository<Product> _repository;

        public ProductService(IMapper mapper, FoodyContext context) : base(mapper,context)
        {
            _mapper = mapper;
            _repository = new GenericRepository<Product>(context);
        }
    }
}
