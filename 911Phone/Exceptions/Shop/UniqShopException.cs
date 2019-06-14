using System;
using System.Runtime.Serialization;

namespace Phone.Exceptions.Shop
{
    /// <summary>
    /// Class-Exception, catching when category has children
    /// <summary>
    [Serializable]
    public class UniqShopException : Exception
    {
        public UniqShopException() : base()
        {
        }

        public UniqShopException(string message) : base(message)
        {
        }

        public UniqShopException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UniqShopException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}


