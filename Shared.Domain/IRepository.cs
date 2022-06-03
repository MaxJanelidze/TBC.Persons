using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Domain
{
    public interface IRepository<TEntity, T_Id>
        where TEntity : class
        where T_Id : struct
    {
        Task<TEntity> OfIdAsync(T_Id id);

        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression = null);

        Task InsertAsync(TEntity entity, CancellationToken cancellationToken);

        void Update(TEntity entity);

        void Delete(TEntity entity);
    }
}
