using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApi.Models.Base;

namespace TodoApi.Repositories.Base
{
    public class BaseRepository<T> : IRepository<T> where T : class, IBaseEntity
    {
        public readonly TodoContext _context;

        public readonly DbSet<T> _dbSet;

        public BaseRepository(TodoContext todoContext)
        {
            _context = todoContext;
            _dbSet = _context.Set<T>();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null)
        {
            IQueryable<T> list = _dbSet.AsQueryable();

            if (predicate is null)
                return list;

            return list.Where(predicate);
        }

        public T GetById(int id)
        {
            IQueryable<T> list = _dbSet.AsQueryable();

            return list.FirstOrDefault(x => x.Id == id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);

            await _context.SaveChangesAsync();
        }
       
        public void Dispose()
        {
            this._context.Dispose();
        }
    }
}
