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
    public class MenuDetailService : BaseService<DTO.MenuDetail, MenuDetail>, IMenuDetailService
    {
        IMapper _mapper;
        IGenericRepository<MenuDetail> _repository;
        FoodyContext _context;

        public MenuDetailService(IMapper mapper, FoodyContext context) : base(mapper, context)
        {
            _mapper = mapper;
            _repository = new GenericRepository<MenuDetail>(context);
            _context = context;
        }

        public async Task<IEnumerable<DTO.MenuDetail>?> Distinct(int orderId, IEnumerable<DTO.MenuDetail> details_update)
        {
            //convert dto to entity
            var details = new List<MenuDetail>();
            foreach (var item in details_update)
            {
                details.Add(_mapper.Map<MenuDetail>(item));
            }
            //get list details in db
            var details_db = await _repository.GetList(filter: detail => detail.MenuId == orderId);
            if(details_db == null)
            {
                return null;
            } else
            {
                List<DTO.MenuDetail> result = new List<DTO.MenuDetail>();
                foreach (var detail in details_db)
                {
                    //find if in db detail contains this product
                    var detail_f = details.FirstOrDefault(d => d.ProductId == detail.ProductId);
                    if(detail_f != null)
                    {
                        //found in passed in detail
                        //add
                        detail.Status = true;
                        result.Add(_mapper.Map<DTO.MenuDetail>(detail));

                    } else
                    {
                        //not found in passed in detail
                        //update
                        detail.Status = false;
                        result.Add(_mapper.Map<DTO.MenuDetail>(detail));
                    }
                }
                return result;
            }
        }
        public override async Task<DTO.MenuDetail> UpdateAsync(int id, DTO.MenuDetail dto)
        {
            //return base.UpdateAsync(id, dto);
            var entity = _mapper.Map<MenuDetail>(dto);
            try
            {
                if (entity == null) throw new ArgumentNullException("entity");
                var found = _context.Find<MenuDetail>(entity.ProductId, id);
#pragma warning disable CS8073 // The result of the expression is always the same since a value of this type is never equal to 'null'
                if (found != null)
                {
                    _context.Entry<MenuDetail>(found).CurrentValues.SetValues(entity);
                }
                await _repository.SaveChangesAsync();
                dto = _mapper.Map<DTO.MenuDetail>(found);
                return dto;
            }
            catch (Exception)
            {
                throw new Exception("UpdateAsync Update Failed");
            }
        }
        public async Task<bool> UpdateDetailsAsync(IEnumerable<DTO.MenuDetail> details_update)
        {
            //throw new NotImplementedException();
            try
            {
                foreach (var detail in details_update)
                {
                    await this.UpdateAsync(detail.MenuId, detail);
                    //await _repository.SaveChangesAsync();
                }
                return true;
            } catch
            {
                throw;
            }
            
        }
    }
}
