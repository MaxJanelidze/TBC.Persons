using MediatR;
using Microsoft.Extensions.Localization;
using Shared.Application.Mediatr;
using Shared.Common.Exceptions;
using System.Threading;
using System.Threading.Tasks;
using TBC.Persons.Domain;
using TBC.Persons.Shared.Resources;

namespace TBC.Persons.Application.Commands.Persons.Delete
{
    public class DeletePersonCommandHandler : ICommandHandler<DeletePersonCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<ResourceStrings> _localizer;

        public DeletePersonCommandHandler(IUnitOfWork unitOfWork, IStringLocalizer<ResourceStrings> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _unitOfWork.PersonRepository.OfIdAsync(request.Id);

            if (person == null)
            {
                throw new NotFoundException(_localizer["PersonNotFound"]);
            }

            _unitOfWork.PersonRepository.Delete(person);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
