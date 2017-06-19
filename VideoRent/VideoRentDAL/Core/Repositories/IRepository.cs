using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace VideoRentDAL.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);


        void Add(TEntity entity);
        void AddRange(IList<TEntity> entities);


        void Remove(int id);
        void RemoveRange(IEnumerable<TEntity> entities);

        void Update(TEntity entityToUpdate);


    }
}