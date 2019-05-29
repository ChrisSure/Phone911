using System;
using System.Runtime.Serialization;

namespace Phone.Exceptions.Catalog
{
    /// <summary>
    /// Class-Exception, catching when category has children
    /// <summary>
    [Serializable]
    public class IssetChildException : Exception
    {
        public IssetChildException() : base()
        {
        }

        public IssetChildException(string message) : base(message)
        {
        }

        public IssetChildException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IssetChildException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

