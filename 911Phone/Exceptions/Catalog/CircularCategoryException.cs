using System;
using System.Runtime.Serialization;

namespace Phone.Exceptions.Catalog
{
    /// <summary>
    /// Class-Exception, catching when category has children
    /// <summary>
    [Serializable]
    public class CircularCategoryException : Exception
    {
        public CircularCategoryException() : base()
        {
        }

        public CircularCategoryException(string message) : base(message)
        {
        }

        public CircularCategoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CircularCategoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}


