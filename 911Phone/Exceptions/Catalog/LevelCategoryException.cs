using System;
using System.Runtime.Serialization;

namespace Phone.Exceptions.Catalog
{
    /// <summary>
    /// Class-Exception, catching when category has children
    /// <summary>
    [Serializable]
    public class LevelCategoryException : Exception
    {
        public LevelCategoryException() : base()
        {
        }

        public LevelCategoryException(string message) : base(message)
        {
        }

        public LevelCategoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LevelCategoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

