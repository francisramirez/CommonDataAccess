

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
        public virtual void Add(TEntity entity) => _entities.AddAsync(entity);
        public virtual void Add(params TEntity[] entities) => _entities.AddRangeAsync(entities);
        public virtual void Add(IEnumerable<TEntity> entities) => _entities.AddRangeAsync(entities);
        public virtual void Remove(TEntity entity) => _entities.Remove(entity);

        public virtual void Remove(params TEntity[] entities) => _entities.RemoveRange(entities);
        public virtual void Remove(IEnumerable<TEntity> entities) => _entities.RemoveRange(entities);
        public virtual void Update(TEntity entity) => _entities.Update(entity);
        public virtual void Update(params TEntity[] entities) => _entities.UpdateRange(entities);
        public virtual void Update(IEnumerable<TEntity> entities) => _entities.UpdateRange(entities);
        public virtual async Task<int> CountAll() => await _entities.CountAsync();
        public virtual async Task<int> CountWhere(Expression<Func<TEntity, bool>> filter) => await _entities.CountAsync(filter);
        public virtual async Task<bool> Exists(Expression<Func<TEntity, bool>> filter) => await _entities.AnyAsync(filter);

        public virtual async Task<TEntity> Find(Expression<Func<TEntity, bool>> filter) => await _entities.SingleOrDefaultAsync(filter);

        public virtual async Task<IEnumerable<TEntity>> GetAll() => await _entities.ToListAsync();

        public virtual async Task<TEntity> GetById(object id) => await _entities.FindAsync(id);

        public virtual async Task<IEnumerable<TEntity>> GetWhere(Expression<Func<TEntity, bool>> filter) => await _entities.Where(filter).ToListAsync();

        public virtual async Task<bool> Commit() => await _context.SaveChangesAsync() > 0;
        public Task<bool> RollBack() => throw new NotImplementedException();
    }
}
