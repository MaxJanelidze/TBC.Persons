using Microsoft.EntityFrameworkCore;
using Shared.Domain;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Infrastructure
{
    public class BaseRepository<TContext, TEntity, T_Id> : IRepository<TEntity, T_Id>
        where TEntity : Entity<T_Id>
        where T_Id : struct
        where TContext : DbContext
    {
        protected readonly TContext _context;

        public BaseRepository(TContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null
                ? _context.Set<TEntity>().AsQueryable().AsNoTracking()
                : _context.Set<TEntity>().AsNoTracking().Where(expression);
        }

        public async Task<TEntity> OfIdAsync(T_Id id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task InsertAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
    }
}
