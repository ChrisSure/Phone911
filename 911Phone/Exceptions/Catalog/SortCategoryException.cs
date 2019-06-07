using System;
using System.Runtime.Serialization;

namespace Phone.Exceptions.Catalog
{
    /// <summary>
    /// Class-Exception, catching when category has children
    /// <summary>
    [Serializable]
    public class SortCategoryException : Exception
    {
        public SortCategoryException() : base()
        {
        }

        public SortCategoryException(string message) : base(message)
        {
        }

        public SortCategoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SortCategoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}


