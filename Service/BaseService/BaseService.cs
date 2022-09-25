using ApplicationCore.Context;
using ApplicationCore.Repository;
using AutoMapper;
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

        public async virtual Task<IEnumerable<TDto>> GetAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            var list_entities = await _repository.GetList(filter);
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
                return null;
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

        public async virtual Task<TDto> UpdateAsync(TDto dto)
        {
            //throw new NotImplementedException();
            if(dto != null)
            {
                var entity = _mapper.Map<TEntity>(dto);
                try
                {
                    _repository.Update(entity);
                    await _repository.SaveChangesAsync();
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
