using System;
using System.Runtime.Serialization;

namespace MidiLib
{
    /// <summary>
    /// Description of NotValidEventException
    /// </summary>
    public class NotValidEventException : Exception, ISerializable
    {
        public NotValidEventException()
        {
        }

        public NotValidEventException(string message)
            : base(message)
        {
        }

        public NotValidEventException(string message, NotValidEventException innerException)
            : base(message, innerException)
        {
        }

        // This constructor is needed for serialization.
        protected NotValidEventException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}