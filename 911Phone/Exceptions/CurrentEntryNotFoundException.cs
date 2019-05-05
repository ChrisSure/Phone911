using System;
using System.Runtime.Serialization;

namespace Phone.Exceptions
{
    /// <summary>
    /// Class-Exception, catching when not found something
    /// <summary>
    [Serializable]
    public class CurrentEntryNotFoundException : Exception
    {
        public CurrentEntryNotFoundException() : base()
        {
        }

        public CurrentEntryNotFoundException(string message) : base(message)
        {
        }

        public CurrentEntryNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CurrentEntryNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
