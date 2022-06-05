using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Domain
{
    public interface IGenericUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        IDbConnection GetDbConnection();
    }
}
