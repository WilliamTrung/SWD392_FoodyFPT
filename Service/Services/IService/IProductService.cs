using ApplicationCore.Models;
using Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.IService
{
    public interface IProductService : IBaseService<DTO.Product, Product>
    {
        public Task<bool> CheckOut(DTO.OrderDetail detail);
    }
}
