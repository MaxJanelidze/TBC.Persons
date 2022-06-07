using Microsoft.AspNetCore.Hosting;
using Shared.Application.Mediatr;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TBC.Persons.Domain;

namespace TBC.Persons.Application.Commands.Persons.UploadPicture
{
    public class UploadPictureCommandHandler : ICommandHandler<UploadPictureCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public UploadPictureCommandHandler(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }

        public async Task<string> Handle(UploadPictureCommand request, CancellationToken cancellationToken)
        {
            var person = await _unitOfWork.PersonRepository.TryGetPerson(request.PersonId);

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
