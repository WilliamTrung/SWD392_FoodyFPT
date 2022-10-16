using ApplicationCore.Context;
using ApplicationCore.Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8603 // Possible null reference return.
namespace Service.Service
{
    public class BaseService<TDto, TEntity> : IBaseService<TDto, TEntity>
        where TEntity : class
        where TDto : class
    {
        IMapper _mapper;
        IGenericRepository<TEntity> _repository;
        FoodyContext _context;
        public BaseService(IMapper mapper,FoodyContext context)
        {
            _mapper = mapper;
            _context = context;
            _repository = new GenericRepository<TEntity>(_context);
        }

        public async virtual Task<TDto> CreateAsync(TDto dto)
        {
            //throw new NotImplementedException();
            if (dto != null)
            {
                var entity = _mapper.Map<TEntity>(dto);
                try
                {
                    _repository.Add(entity);
                    await _repository.SaveChangesAsync();
                    //Modify TrungNT start 26-09-2022
                    dto = _mapper.Map<TDto>(entity);
                    //Modify TrungNT end 26-09-2022
                }
                catch
                {
                    dto = null;
                }
            }
            return dto;
        }

        public async virtual Task<bool> DeleteAsync(int id)
        {
            //throw new NotImplementedException();
            var entity = await _repository.FindByIdAsync(id);
            if(entity != null)
            {
                _repository.Delete(entity);
                return await _repository.SaveChangesAsync() > 0;
            } else
            {
                return false;
            }
        }

        public virtual TEntity DisableSelfReference(TEntity entity)
        {
            //throw new NotImplementedException();
            return entity;
        }

        public async virtual Task<IEnumerable<TDto>> GetAsync(PagingRequest? paging = null,Expression<Func<TEntity, bool>>? filter = null, string? includeProperties = null)
        {
            var list_entities_raw = (await _repository.GetList(filter, includeProperties)).AsEnumerable();
            //Paging apply
            if (paging == null || paging.PageIndex <= 0 || paging.PageSize <= 0)
                paging = new PagingRequest();
            list_entities_raw = list_entities_raw.Skip((paging.PageIndex - 1) * paging.PageSize)
               .Take(paging.PageSize);

            var list_entities = new List<TEntity>();
            for (int i = 0; i< list_entities_raw.Count(); i++)
            {
                var entity = list_entities_raw.ElementAt(i);
                entity = DisableSelfReference(entity);
                list_entities.Add(entity);
            }
            //await list_entities.ForEachAsync(e => e = DisableSelfReference(e));
            /*
            foreach(var entity in list_entities)
            {
                DisableSelfReference(ref entity);
            }
            */
            if(list_entities != null && list_entities.Count() > 0)
            {
                List<TDto> list_dto = new List<TDto>();
                foreach (var entity in list_entities)
                {
                    var dto = _mapper.Map<TDto>(entity);
                    list_dto.Add(dto);
                }
                return list_dto;
            } 
            else
            {
                return new List<TDto>();
            }
            //throw new NotImplementedException();
        }

        public async virtual Task<TDto> GetByIdAsync(int id)
        {
            //throw new NotImplementedException();
            var entity = await _repository.FindByIdAsync(id);
            if(entity != null)
            {
                var dto = _mapper.Map<TDto>(entity);
                return dto;
            }
            return null;
        }

        public async virtual Task<TDto> UpdateAsync(int id, TDto dto)
        {
            //throw new NotImplementedException();
            if(dto != null)
            {
                var entity = _mapper.Map<TEntity>(dto);
                try
                {
                    //Modify TrungNT start 26-09-2022
                    /*var found = await _context.FindAsync<TEntity>(id);
                    if (found != null)
                    {
                        _context.Entry(found).CurrentValues.SetValues(entity);
                        //_repository.Update(entity);
                        await _repository.SaveChangesAsync();
                        //Modify TrungNT start 26-09-2022
                        dto = _mapper.Map<TDto>(entity);
                        //Modify TrungNT end 26-09-2022
                    } else
                    {
                        dto = null;
                    }*/
                    //Modify TrungNT end 26-09-2022

                    //Modify AnhTN start 26 - 09 - 2022
                    _repository.Update(id, entity);
                    await _repository.SaveChangesAsync();
                    dto = _mapper.Map<TDto>(entity);
                    //Modify AnhTN end 26-09-2022
                }
                catch
                {

                    dto = null;
                }
                
            }
            return dto;
        }
    }
}
