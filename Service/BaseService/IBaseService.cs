using Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public interface IBaseService<TDto, TEntity> 
        where TEntity : class 
        where TDto : class
    {
        Task<TDto> CreateAsync(TDto dto);
        Task<TDto> UpdateAsync(int id, TDto dto);
        Task<bool> DeleteAsync(int id);
        Task<TDto> GetByIdAsync(int id);
        Task<IEnumerable<TDto>> GetAsync(PagingRequest? paging = null, Expression<Func<TEntity, bool>>? filter = null, string? includeProperties = null);
        public TEntity DisableSelfReference(TEntity entity);

    }
}
