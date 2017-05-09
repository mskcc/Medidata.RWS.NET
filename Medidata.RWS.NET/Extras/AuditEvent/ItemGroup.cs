using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medidata.RWS.Extras.AuditEvent
{

    /// <summary>
    /// ItemGroup related attributes.
    /// </summary>
    /// <seealso cref="Medidata.RWS.Extras.AuditEvent.ContextContainer" />
    public class ItemGroup : ContextContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemGroup"/> class.
        /// </summary>
        /// <param name="OID">The oid.</param>
        /// <param name="RepeatKey">The repeat key.</param>
        /// <param name="TransactionType">Type of the transaction.</param>
        /// <param name="RecordId">The record identifier.</param>
        public ItemGroup(string OID, int RepeatKey, string TransactionType, int RecordId) :
            base(OID, RepeatKey, TransactionType)
        {
            this.RecordId = RecordId;
        }

        /// <summary>
        /// Gets the record identifier.
        /// </summary>
        /// <value>
        /// The record identifier.
        /// </value>
        public int RecordId { get; private set; }
    }
}
