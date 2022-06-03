using System.Net;

namespace Shared.Common.Exceptions
{
    public class NotFoundException : ClientErrorException
    {
        private const int _statusCode = (int)HttpStatusCode.NotFound;

        public NotFoundException(string message)
            : base(message, _statusCode)
        {
        }
    }
}
