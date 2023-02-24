using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TodoApi.Repositories.Base
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null);

        T GetById(int id);

        Task AddAsync(T entity);
    }
}
