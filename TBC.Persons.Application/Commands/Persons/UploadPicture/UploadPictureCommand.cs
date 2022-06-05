using Microsoft.AspNetCore.Http;
using Shared.Application.Mediatr;

namespace TBC.Persons.Application.Commands.Persons.UploadPicture
{
    public class UploadPictureCommand : ICommand<string>
    {
        public int PersonId { get; set; }

        public IFormFile Picture { get; set; }
    }
}
