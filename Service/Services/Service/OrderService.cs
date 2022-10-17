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
    public class OrderService : BaseService<DTO.Order, Order>, IOrderService
    {
        IMapper _mapper;
        IGenericRepository<Order> _repository;
        IOrderDetailService _orderDetailService;
        IProductService _productService;

        public OrderService(IMapper mapper, FoodyContext context) : base(mapper, context)
        {
            _mapper = mapper;
            _repository = new GenericRepository<Order>(context);
            _orderDetailService = new OrderDetailService(_mapper, context);
            _productService = new ProductService(_mapper, context);
        }

        public async Task<bool> CheckOut(DTO.Order order)
        {
            var list_detail = new List<DTO.OrderDetail>();
            if(order != null)
            {
                await base.UpdateAsync(order.Id, order);//update status paid or not
                if(order.OrderDetails == null || order.OrderDetails.Count == 0)
                {
                    return false;
                }
                else
                {
                    foreach(DTO.OrderDetail detail in order.OrderDetails)
                    {
                        list_detail.Add(detail);
                    }
                }
                if(list_detail.Count > 0)
                {
                    foreach(DTO.OrderDetail detail in list_detail)
                    {
                         await _productService.CheckOut(detail);
                    }
                    return true;
                }
            }
            return false;
        }

        public override Task<IEnumerable<DTO.Order>> GetAsync(PagingRequest? paging = null, Expression<Func<Order, bool>>? filter = null, string? includeProperties = null)
        {
            var result = base.GetAsync(filter: filter);
            foreach (var item in result.Result)
            {
                var details = _orderDetailService.GetAsync(filter: detail => detail.OrderId == item.Id).Result.ToList();
                if (details != null)
                {
                    item.OrderDetails = details;
                }

            }
            foreach (var item in result.Result)
            {
                DisableSelfReference(_mapper.Map<Order>(item));
            }
            return result;
        }
    }
}
