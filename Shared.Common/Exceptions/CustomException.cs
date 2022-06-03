using System;

namespace Shared.Common.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException(string message, int statusCode)
           : base(message)
        {
            StatusCode = statusCode;
        }

        public int StatusCode { get; set; }
    }
}
