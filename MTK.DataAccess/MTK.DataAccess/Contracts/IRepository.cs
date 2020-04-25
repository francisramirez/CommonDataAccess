namespace MTK.DataAccess.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    public interface IRepository<TEntity> : IUnitOfWork where TEntity: class
    {
        Task<TEntity> GetById(object id);
        Task<TEntity> Find(Expression<Func<TEntity, bool>> filter);
        void Add(TEntity entity);
        void Add(params TEntity[] entities);
        void Add(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Update(params TEntity[] entities);
        void Update(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void Remove(params TEntity[] entities);
        void Remove(IEnumerable<TEntity> entities);
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> GetWhere(Expression<Func<TEntity, bool>> filter);
        Task<int> CountAll();
        Task<int> CountWhere(Expression<Func<TEntity, bool>> filter);
        Task<bool> Exists(Expression<Func<TEntity, bool>> filter);

    }
}
