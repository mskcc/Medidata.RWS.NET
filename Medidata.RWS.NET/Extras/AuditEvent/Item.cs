using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medidata.RWS.Extras.AuditEvent
{

    /// <summary>
    /// Item related attributes.
    /// </summary>
    /// <seealso cref="Medidata.RWS.Extras.AuditEvent.ContextBase" />
    public class Item : ContextBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="OID">The oid.</param>
        /// <param name="Value">The value.</param>
        /// <param name="Freeze">if set to <c>true</c> [freeze].</param>
        /// <param name="Verify">if set to <c>true</c> [verify].</param>
        /// <param name="Lock">if set to <c>true</c> [lock].</param>
        /// <param name="TransactionType">Type of the transaction.</param>
        public Item(string OID, string Value, bool Freeze, bool Verify, bool Lock, string TransactionType)
        {
            this.OID = OID;
            this.Value = Value;
            this.Freeze = Freeze;
            this.Verify = Verify;
            this.Lock = Lock;
            this.TransactionType = TransactionType;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Item"/> is freeze.
        /// </summary>
        /// <value>
        ///   <c>true</c> if freeze; otherwise, <c>false</c>.
        /// </value>
        public bool Freeze { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="Item"/> is lock.
        /// </summary>
        /// <value>
        ///   <c>true</c> if lock; otherwise, <c>false</c>.
        /// </value>
        public bool Lock { get; private set; }
        /// <summary>
        /// Gets the oid.
        /// </summary>
        /// <value>
        /// The oid.
        /// </value>
        public string OID { get; private set; }
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
        /// <summary>
        /// Gets a value indicating whether this <see cref="Item"/> is verify.
        /// </summary>
        /// <value>
        ///   <c>true</c> if verify; otherwise, <c>false</c>.
        /// </value>
        public bool Verify { get; private set; }
    }
}
