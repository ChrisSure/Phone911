using System;
using System.Runtime.Serialization;

namespace Phone.Exceptions.Catalog
{
    /// <summary>
    /// Class-Exception, catching when category has children
    /// <summary>
    [Serializable]
    public class CategoryHasProducts : Exception
    {
        public CategoryHasProducts() : base()
        {
        }

        public CategoryHasProducts(string message) : base(message)
        {
        }

        public CategoryHasProducts(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CategoryHasProducts(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}