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
    public class SlotService : BaseService<DTO.Slot, Slot>, ISlotService
    {
        IMapper _mapper;
        IGenericRepository<Slot> _repository;
        IGenericRepository<MenuDetail> _repositoryMenuDetails;
        public SlotService(IMapper mapper, FoodyContext context) : base(mapper, context)
        {
            _mapper = mapper;
            _repository = new GenericRepository<Slot>(context);
            _repositoryMenuDetails = new GenericRepository<MenuDetail>(context);
        }
        public override Slot DisableSelfReference(Slot entity)
        {
            if (entity.Menu != null)
            {
                entity.Menu.Slots = null;
            }
            return entity;
        }
        public override Task<IEnumerable<DTO.Slot>> GetAsync(PagingRequest? paging = null, Expression<Func<Slot, bool>>? filter = null, string? includeProperties = null)
        {
            var list = base.GetAsync(paging, filter, includeProperties);
            foreach (var item in list.Result)
            {
                DisableSelfReference(_mapper.Map<Slot>(item));
                /*
                if(item.Menu != null)
                {
                    var details_models = new List<DTO.MenuDetail>();
                    var details = _repositoryMenuDetails.GetList(filter: detail => detail.MenuId == item.Menu.Id).Result;
                    foreach (var detail in details)
                    {
                        details_models.Add(_mapper.Map<DTO.MenuDetail>(detail));
                    }
                    if(details_models != null && details_models.Count() > 0)
                    {
                        item.Menu.MenuDetails = details_models;
                    }
                }
                */
            }
            return list;
        }
    }
}
