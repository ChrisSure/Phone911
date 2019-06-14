using System;
using System.Runtime.Serialization;

namespace Phone.Exceptions.Shop
{
    /// <summary>
    /// Class-Exception, catching when category has children
    /// <summary>
    [Serializable]
    public class LevelShopCategoryException : Exception
    {
        public LevelShopCategoryException() : base()
        {
        }

        public LevelShopCategoryException(string message) : base(message)
        {
        }

        public LevelShopCategoryException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LevelShopCategoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}


