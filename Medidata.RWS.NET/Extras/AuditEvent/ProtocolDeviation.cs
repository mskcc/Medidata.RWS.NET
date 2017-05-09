using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medidata.RWS.Extras.AuditEvent
{

    /// <summary>
    /// Protocol Deviation related attributes.
    /// </summary>
    /// <seealso cref="Medidata.RWS.Extras.AuditEvent.ContextBase" />
    public class ProtocolDeviation : ContextBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ProtocolDeviation"/> class.
        /// </summary>
        /// <param name="RepeatKey">The repeat key.</param>
        /// <param name="Code">The code.</param>
        /// <param name="Class">The class.</param>
        /// <param name="Status">The status.</param>
        /// <param name="Value">The value.</param>
        /// <param name="TransactionType">Type of the transaction.</param>
        public ProtocolDeviation(int RepeatKey, string Code, string Class, string Status, string Value, string TransactionType)
        {

            this.RepeatKey = RepeatKey;
            this.Code = Code;
            this.Class = Class;
            this.Status = Status;
            this.Value = Value;
            this.TransactionType = TransactionType;

        }

        /// <summary>
        /// Gets the class.
        /// </summary>
        /// <value>
        /// The class.
        /// </value>
        public string Class { get; private set; }
        /// <summary>
        /// Gets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code { get; private set; }
        /// <summary>
        /// Gets the repeat key.
        /// </summary>
        /// <value>
        /// The repeat key.
        /// </value>
        public int RepeatKey { get; private set; }
        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; private set; }
        /// <summary>
        /// Gets the type of the transaction.
        /// </summary>
        /// <value>
        /// The type of the transaction.
        /// </value>
        public string TransactionType { get; private set; }
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; private set; }
    }
}
