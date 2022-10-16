using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        //Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> FindByIdAsync(int id);
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void Update(int id, TEntity entity);
        Task<int> SaveChangesAsync();
        Task<IQueryable<TEntity>> GetList(Expression<Func<TEntity, bool>>? filter = null, string? includeProperties = null);
    }
}
