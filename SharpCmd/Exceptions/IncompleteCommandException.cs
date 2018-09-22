using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCmd.Exceptions
{
    public class IncompleteCommandException : Exception
    {
        public IncompleteCommandException() : base()
        {
        }

        protected IncompleteCommandException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public IncompleteCommandException(string message) : base(message)
        {
        }

        public IncompleteCommandException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
