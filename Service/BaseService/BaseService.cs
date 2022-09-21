using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class BaseService<TDto, TEntity> : IBaseService<TDto, TEntity>
        where TEntity : class
        where TDto : class
    {
        public Task<TDto> CreateAsync(TDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TDto> UpdateAsync(TDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
