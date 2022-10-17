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
    public class OrderDetailService : BaseService<DTO.OrderDetail, OrderDetail>, IOrderDetailService
    {
        IMapper _mapper;
        IGenericRepository<OrderDetail> _repository;
        FoodyContext _context;
        public OrderDetailService(IMapper mapper, FoodyContext context) : base(mapper, context)
        {
            _mapper = mapper;
            _context = context;
            _repository = new GenericRepository<OrderDetail>(context);
        }

        public override async Task<DTO.OrderDetail> UpdateAsync(int id, DTO.OrderDetail dto)
        {
            var entity = _mapper.Map<OrderDetail>(dto);
            try
            {
                if (entity == null) throw new ArgumentNullException("entity");
                var found = _context.Find<OrderDetail>(id, entity.ProductId);
#pragma warning disable CS8073 // The result of the expression is always the same since a value of this type is never equal to 'null'
                if (found != null)
                {
                    _context.Entry<OrderDetail>(found).CurrentValues.SetValues(entity);
                }
                await _repository.SaveChangesAsync();
                dto = _mapper.Map<DTO.OrderDetail>(found);
                return dto;
            }
            catch (Exception)
            {
                throw new Exception("UpdateAsync Update Failed");
            }
        }

        public async Task<bool> UpdateDetailsAsync(IEnumerable<DTO.OrderDetail> details_update)
        {
            try
            {
                foreach (var detail in details_update)
                {
                    await this.UpdateAsync(detail.OrderId, detail);
                }
                return true;
            }
            catch
            {
                throw;
            }
            //throw new NotImplementedException();
        }
    }
}
