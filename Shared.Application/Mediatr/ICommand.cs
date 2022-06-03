using MediatR;

namespace Shared.Application.Mediatr
{
    public interface ICommand<T> : IRequest<T>
    {
    }
}
