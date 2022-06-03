using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace TBC.Persons.Infrastructure.Persistence.Context
{
    public class PersonDbContext : DbContext
    {
        private IDbContextTransaction _currentTransaction;

        public PersonDbContext(DbContextOptions<PersonDbContext> options)
            : base(options)
        {
        }

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public bool HasActiveTransaction => _currentTransaction != null;

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(cancellationToken);

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();

                transaction.Commit();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                DisposeTranstaction();
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                DisposeTranstaction();
            }
        }

        public void DisposeTranstaction()
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
