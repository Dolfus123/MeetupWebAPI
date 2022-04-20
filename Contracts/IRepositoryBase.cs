using System;
using System.Linq;
using System.Linq.Expressions;

namespace MeetupWebAPI.Contracts
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAllAsync();
        IQueryable<T> FindByConditionAsync(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
