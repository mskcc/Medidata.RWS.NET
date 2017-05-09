using System;
using System.Runtime.Serialization;
using Medidata.RWS.Core.Responses;

namespace Medidata.RWS.Core.Exceptions
{
    /// <summary>
    /// Represents a RAVE Web Services Exception.
    /// </summary>
    /// <seealso cref="System.Exception" />
    [Serializable]
    public class RWSException : Exception
    {
        /// <summary>
        /// Gets the RWS error response.
        /// </summary>
        /// <value>
        /// The RWS error response.
        /// </value>
        public RWSErrorResponse rws_error_response { get; private set; }
        /// <summary>
        /// Gets the RWS error.
        /// </summary>
        /// <value>
        /// The RWS error.
        /// </value>
        public RwsError rws_error { get; private set; }
        /// <summary>
        /// Gets the error description.
        /// </summary>
        /// <value>
        /// The error description.
        /// </value>
        public string errorDescription { get; private set; }
        /// <summary>
        /// Gets the error title.
        /// </summary>
        /// <value>
        /// The error title.
        /// </value>
        public string errorTitle { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RWSException"/> class.
        /// </summary>
        public RWSException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RWSException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public RWSException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RWSException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public RWSException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RWSException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="rws_error">The RWS error.</param>
        public RWSException(string message, RwsError rws_error) : this(message)
        {
            this.rws_error = rws_error;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RWSException"/> class.
        /// </summary>
        /// <param name="errorTitle">The error title.</param>
        /// <param name="errorDescription">The error description.</param>
        public RWSException(string errorTitle, string errorDescription) : base(errorTitle)
        {
            this.errorDescription = errorDescription;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RWSException"/> class.
        /// </summary>
        /// <param name="errorDescription">The error description.</param>
        /// <param name="rws_error_response">The RWS error response.</param>
        public RWSException(string errorDescription, RWSErrorResponse rws_error_response) : base(errorDescription)
        {
            this.errorDescription = errorDescription;
            this.rws_error_response = rws_error_response;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RWSException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected RWSException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}