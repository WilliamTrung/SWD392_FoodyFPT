using ApplicationCore.Models;
using Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.IService
{
    public interface IOrderDetailService : IBaseService<DTO.OrderDetail, OrderDetail>
    {
        public Task<bool> UpdateDetailsAsync(IEnumerable<DTO.OrderDetail> details_update);
    }
}
