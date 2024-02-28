using System.Runtime.Serialization;

namespace SistemaEsterno.Services
{
    [Serializable]
    internal class EmptyResultException : Exception
    {
        public EmptyResultException()
        {
        }

        public EmptyResultException(string? message) : base(message)
        {
        }

        public EmptyResultException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EmptyResultException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
