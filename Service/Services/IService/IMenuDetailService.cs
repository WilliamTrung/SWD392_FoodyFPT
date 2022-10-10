using ApplicationCore.Models;
using Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.IService
{
    public interface IMenuDetailService : IBaseService<DTO.MenuDetail, MenuDetail>
    {
        public Task<IEnumerable<DTO.MenuDetail>?> Distinct(int orderId, IEnumerable<DTO.MenuDetail> details_update);
        public Task<bool> UpdateDetailsAsync(IEnumerable<DTO.MenuDetail> details_update);
    }
}
