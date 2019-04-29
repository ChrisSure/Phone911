using System;
using System.Runtime.Serialization;

namespace Phone.Exceptions
{
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
