using System;
using System.Runtime.Serialization;

namespace Ext.Direct
{
    /// <summary>
    /// Thrown when an error occurs during a Ext.Direct call.
    /// </summary>
    public class DirectException : ApplicationException
    {
        #region Constructors
        /// <summary>
        /// Initialises an instance of this class
        /// </summary>
        public DirectException()
            : base()
        {
        }

        /// <summary>
        /// Initialises an instance of this class
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        public DirectException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initialises an instance of this class
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference
        /// if no inner exception is specified</param>
        public DirectException(string message,
                               System.Exception innerException)
            : base(message,
                   innerException)
        {
        }

        /// <summary>
        /// Initialises an instance of this class
        /// </summary>
        /// <param name="info">
        /// The System.Runtime.Serialization.SerializationInfo that holds the serialized
        /// object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The System.Runtime.Serialization.StreamingContext that contains contextual
        /// information about the source or destination.
        /// </param>
        protected DirectException(SerializationInfo info,
                                  StreamingContext  context)
            : base(info,
                   context)
        {
        }
        #endregion
    }
}