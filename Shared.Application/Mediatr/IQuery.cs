using MediatR;

namespace Shared.Application.Mediatr
{
    public interface IQuery<T> : IRequest<T>
    {
    }
}
