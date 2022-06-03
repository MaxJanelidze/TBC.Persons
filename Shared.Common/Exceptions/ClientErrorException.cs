using System.Net;

namespace Shared.Common.Exceptions
{
    public class ClientErrorException : CustomException
    {
        private const int _statusCode = (int)HttpStatusCode.BadRequest;

        /// <summary>
        /// Default status code is 400 Bad Request
        /// </summary>
        public ClientErrorException(string message)
            : base(message, _statusCode)
        {
        }

        public ClientErrorException(string message, int statusCode)
            : base(message, statusCode)
        {
        }
    }
}
