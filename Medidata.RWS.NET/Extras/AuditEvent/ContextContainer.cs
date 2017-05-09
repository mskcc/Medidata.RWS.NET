using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medidata.RWS.Extras.AuditEvent
{

    /// <summary>
    /// Base classes for ODM containers that have an oid and repeat key
    /// </summary>
    /// <seealso cref="Medidata.RWS.Extras.AuditEvent.ContextBase" />
    public class ContextContainer : ContextBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ContextContainer"/> class.
        /// </summary>
        /// <param name="OID">The oid.</param>
        /// <param name="RepeatKey">The repeat key.</param>
        /// <param name="TransactionType">Type of the transaction.</param>
        public ContextContainer(string OID, object RepeatKey, string TransactionType)
        {

            this.OID = OID;
            this.RepeatKey = RepeatKey.ToString();
            this.TransactionType = TransactionType;
        }

        /// <summary>
        /// Gets the oid.
        /// </summary>
        /// <value>
        /// The oid.
        /// </value>
        public string OID { get; private set; }
        /// <summary>
        /// Gets the repeat key.
        /// </summary>
        /// <value>
        /// The repeat key.
        /// </value>
        public string RepeatKey { get; private set; }
        /// <summary>
        /// Gets the type of the transaction.
        /// </summary>
        /// <value>
        /// The type of the transaction.
        /// </value>
        public string TransactionType { get; private set; }

    }
}
