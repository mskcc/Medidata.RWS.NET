using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medidata.RWS.Extras.AuditEvent
{

    /// <summary>
    /// Form related attributes.
    /// </summary>
    /// <seealso cref="Medidata.RWS.Extras.AuditEvent.ContextContainer" />
    public class Form : ContextContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Form"/> class.
        /// </summary>
        /// <param name="OID">The oid.</param>
        /// <param name="RepeatKey">The repeat key.</param>
        /// <param name="TransactionType">Type of the transaction.</param>
        /// <param name="DataPageName">Name of the data page.</param>
        /// <param name="DataPageId">The data page identifier.</param>
        public Form(string OID, int RepeatKey, string TransactionType, string DataPageName, int DataPageId) : 
            base(OID, RepeatKey, TransactionType)
        {

            this.DataPageName = DataPageName;
            this.DataPageId = DataPageId;
        }

        /// <summary>
        /// Gets the data page identifier.
        /// </summary>
        /// <value>
        /// The data page identifier.
        /// </value>
        public int DataPageId { get; private set; }
        /// <summary>
        /// Gets the name of the data page.
        /// </summary>
        /// <value>
        /// The name of the data page.
        /// </value>
        public string DataPageName { get; private set; }
    }
}
