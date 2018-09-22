using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCmd.Exceptions
{
    public class InvalidCommandException : Exception
    {
        public InvalidCommandException() : base()
        {
        }

        protected InvalidCommandException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public InvalidCommandException(string message) : base(message)
        {
        }

        public InvalidCommandException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
