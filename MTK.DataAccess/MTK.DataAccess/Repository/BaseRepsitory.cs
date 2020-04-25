 

namespace MTK.DataAccess.Repository
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using MTK.DataAccess.Contracts;
    public class BaseRepsitory<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;

        private DbSet<TEntity> _entities;
        public BaseRepsitory(DbContext context)
        {
            this._context = context;
            this._entities = _context.Set<TEntity>();
        }
        public virtual async Task Add(TEntity entity) => await _entities.AddAsync(entity);

        public virtual async Task Add(params TEntity[] entities) => await _entities.AddRangeAsync(entities);
        public virtual async Task Add(IEnumerable<TEntity> entities) => await _entities.AddRangeAsync(entities);
        public virtual async Task<int> CountAll() => await _entities.CountAsync();
        public virtual async Task<int> CountWhere(Expression<Func<TEntity, bool>> filter) => await _entities.CountAsync(filter);

        public virtual async Task<bool> Exists(Expression<Func<TEntity, bool>> filter) => await _entities.AnyAsync(filter);

        public virtual async Task<TEntity> Find(Expression<Func<TEntity, bool>> filter) => await _entities.SingleOrDefaultAsync(filter);

        public virtual async Task<IEnumerable<TEntity>> GetAll() => await _entities.ToListAsync();

        public virtual async Task<TEntity> GetById(object id) => await _entities.FindAsync(id);

        public virtual async Task<IEnumerable<TEntity>> GetWhere(Expression<Func<TEntity, bool>> filter) => await _entities.Where(filter).ToListAsync();


        public virtual async Task Remove(TEntity entity)
        {
            _entities.Remove(entity);
            await this.Commit();
        }

        public virtual async Task Remove(object id)
        {
            var obj = await this.GetById(id);
            _entities.Remove(obj);
            await Commit();
        }


        public virtual async Task Remove(params TEntity[] entities)
        {
            _entities.RemoveRange(entities);
            await this.Commit();
        }

        public virtual async Task Remove(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
            await this.Commit();
        }

        public Task<bool> RollBack()
        {
            throw new NotImplementedException();
        }

        public virtual async Task Update(TEntity entity)
        {
            _entities.Update(entity);
            await this.Commit();
        }

        public virtual async Task Update(params TEntity[] entities)
        {
            _entities.UpdateRange(entities);
            await this.Commit();
        }
        public virtual async Task Update(IEnumerable<TEntity> entities)
        {
            _entities.UpdateRange(entities);
            await this.Commit();
        }
        public virtual async Task<bool> Commit() => await _context.SaveChangesAsync() > 0;
    }
}
