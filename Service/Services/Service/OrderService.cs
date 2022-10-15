using ApplicationCore.Context;
using ApplicationCore.Models;
using ApplicationCore.Repository;
using AutoMapper;
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
    public class OrderService : BaseService<DTO.Order, Order>, IOrderService
    {
        IMapper _mapper;
        IGenericRepository<Order> _repository;
        IOrderDetailService _orderDetailService;

        public OrderService(IMapper mapper, FoodyContext context) : base(mapper, context)
        {
            _mapper = mapper;
            _repository = new GenericRepository<Order>(context);
            _orderDetailService = new OrderDetailService(_mapper, context);
        }

        public override Task<IEnumerable<DTO.Order>> GetAsync(Expression<Func<Order, bool>>? filter = null)
        {
            var result = base.GetAsync(filter);
            foreach (var item in result.Result)
            {
                var details = _orderDetailService.GetAsync(detail => detail.OrderId == item.Id).Result.ToList();
                if (details != null)
                {
                    item.OrderDetails = details;
                }

            }
            return result;
        }
    }
}
