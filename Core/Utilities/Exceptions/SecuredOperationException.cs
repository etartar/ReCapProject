using System;

namespace Core.Utilities.Exceptions
{
    public class SecuredOperationException : Exception
    {
        public SecuredOperationException()
        {
        }

        public SecuredOperationException(string message) : base(message)
        {
        }

        public SecuredOperationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
