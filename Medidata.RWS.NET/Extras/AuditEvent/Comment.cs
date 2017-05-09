using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medidata.RWS.Extras.AuditEvent
{
    /// <summary>
    /// Comment related attributes
    /// </summary>
    public class Comment : ContextBase
    {
        /// <summary>
        /// Gets the repeat key.
        /// </summary>
        /// <value>
        /// The repeat key.
        /// </value>
        public int RepeatKey { get; private set; }
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; private set; }
        /// <summary>
        /// Gets the type of the transaction.
        /// </summary>
        /// <value>
        /// The type of the transaction.
        /// </value>
        public string TransactionType { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Comment"/> class.
        /// </summary>
        /// <param name="RepeatKey">The repeat key.</param>
        /// <param name="Value">The value.</param>
        /// <param name="TransactionType">Type of the transaction.</param>
        public Comment(int RepeatKey, string Value, string TransactionType)
        {
            this.RepeatKey = RepeatKey;
            this.Value = Value;
            this.TransactionType = TransactionType;

        }
    }
}
