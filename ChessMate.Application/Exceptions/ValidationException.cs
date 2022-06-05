using System;

namespace ChessMate.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public string PropertyName { get; private set; }
        public ValidationException(string propertyName, string message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
