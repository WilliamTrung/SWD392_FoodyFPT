using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ApplicationCore.Context;

namespace ApplicationCore.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal FoodyContext _context;
        public GenericRepository(FoodyContext context)
        {
            _context = context;
        }
        public void Add(TEntity entity)
        {
            try
            {
                if (entity == null) throw new ArgumentNullException("entity");
                _context.Add(entity);
            }
            catch (Exception)
            {
                throw new Exception("GenericRepository Add Failed");
            }
            //throw new NotImplementedException();
        }

        public void Delete(TEntity entity)
        {
            try
            {
                if (entity == null) throw new ArgumentNullException("entity");
                _context.Remove(entity);
            }
            catch (Exception)
            {
                throw new Exception("GenericRepository Delete Failed");
            }
            //throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> FindByIdAsync(int id)
        {
            try
            {
#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                if (id == null) throw new ArgumentNullException("Id");
                else
                {
                    var entity = await _context.FindAsync<TEntity>(id);
#pragma warning disable CS8603 // Possible null reference return.
                    return entity;
                }
            }
            catch (Exception)
            {
                throw new Exception("GenericRepository Delete Failed");
            }
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            //throw new NotImplementedException();
            var list = await _context.Set<TEntity>().ToListAsync();
            return list;
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
            //throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            //throw new NotImplementedException();
            try
            {
                if (entity == null) throw new ArgumentNullException("entity");
                _context.Update(entity);
            }
            catch (Exception)
            {
                throw new Exception("GenericRepository Update Failed");
            }
        }
    }
}
