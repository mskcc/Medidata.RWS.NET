using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medidata.RWS.Extras.AuditEvent
{

    /// <summary>
    /// Query related attributes
    /// </summary>
    /// <seealso cref="Medidata.RWS.Extras.AuditEvent.ContextBase" />
    public class Query : ContextBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Query"/> class.
        /// </summary>
        /// <param name="RepeatKey">The repeat key.</param>
        /// <param name="Status">The status.</param>
        /// <param name="Response">The response.</param>
        /// <param name="Recipient">The recipient.</param>
        /// <param name="Value">The value.</param>
        public Query(int RepeatKey, string Status, string Response, string Recipient, string Value)
        {
            this.RepeatKey = RepeatKey;
            this.Status = Status;
            this.Response = Response;
            this.Recipient = Recipient;
            this.Value = Value;

        }

        /// <summary>
        /// Gets the recipient.
        /// </summary>
        /// <value>
        /// The recipient.
        /// </value>
        public string Recipient { get; private set; }
        /// <summary>
        /// Gets the repeat key.
        /// </summary>
        /// <value>
        /// The repeat key.
        /// </value>
        public int RepeatKey { get; private set; }
        /// <summary>
        /// Gets the response.
        /// </summary>
        /// <value>
        /// The response.
        /// </value>
        public string Response { get; private set; }
        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; private set; }
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; private set; }
    }
}
