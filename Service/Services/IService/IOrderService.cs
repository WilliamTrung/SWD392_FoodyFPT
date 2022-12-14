using ApplicationCore.Models;
using Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.IService
{
    public interface IOrderService: IBaseService<DTO.Order, Order>
    {
        public Task<bool> CheckOut(DTO.Order order);
    }
}
