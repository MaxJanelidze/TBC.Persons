using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using Shared.Application.Mediatr;
using System;
using System.Threading;
using System.Threading.Tasks;
using TBC.Persons.Infrastructure.Persistence.Context;

namespace TBC.Persons.Infrastructure.Persistence.Behaviours
{
    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly PersonDbContext _context;

        public TransactionBehaviour(PersonDbContext context)
        {
            _context = context;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_context.HasActiveTransaction || !(request is ICommand<TResponse>))
            {
                return await next();
            }

            try
            {
                IDbContextTransaction transaction = await _context.BeginTransactionAsync(cancellationToken);
                
                TResponse response = await next();

                await _context.CommitTransactionAsync(transaction);

                return response;
            }
            catch (Exception ex)
            {
                _context.RollbackTransaction();
                ex.Data.Add("PreviousStackTrace", ex.StackTrace);

                throw ex;
            }
        }
    }
}
