using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Localization;
using Shared.Application.Mediatr;
using Shared.Common.Exceptions;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TBC.Persons.Domain;
using TBC.Persons.Shared.Resources;

namespace TBC.Persons.Application.Commands.Persons.UploadPicture
{
    public class UploadPictureCommandHandler : ICommandHandler<UploadPictureCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<ResourceStrings> _localizer;
        private readonly IWebHostEnvironment _env;

        public UploadPictureCommandHandler(IUnitOfWork unitOfWork, IStringLocalizer<ResourceStrings> localizer, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
            _env = env;
        }

        public async Task<string> Handle(UploadPictureCommand request, CancellationToken cancellationToken)
        {
            var person = await _unitOfWork.PersonRepository.OfIdAsync(request.PersonId);

            if (person == null)
            {
                throw new NotFoundException(_localizer["PersonNotFound"]);
            };

            var pathToSave = Path.Combine(_env.ContentRootPath, Path.Combine("Resources", "Pictures"));
            var fullPath = Path.Combine(pathToSave, request.Picture.FileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                request.Picture.CopyTo(stream);

                person.AddPictureAddress(fullPath);

                _unitOfWork.PersonRepository.Update(person);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return fullPath;
            }
        }
    }
}
