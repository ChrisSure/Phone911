using System;
using System.Runtime.Serialization;

namespace Phone.Exceptions.User
{
    [Serializable]
    public class UserUniqueConstrainException : Exception
    {
        public UserUniqueConstrainException() : base()
        {
        }

        public UserUniqueConstrainException(string message) : base(message)
        {
        }

        public UserUniqueConstrainException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserUniqueConstrainException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
