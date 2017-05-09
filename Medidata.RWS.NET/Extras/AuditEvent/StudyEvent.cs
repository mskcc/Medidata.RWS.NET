using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medidata.RWS.Extras.AuditEvent
{

    /// <summary>
    /// StudyEvent related attributes
    /// </summary>
    /// <seealso cref="Medidata.RWS.Extras.AuditEvent.ContextContainer" />
    public class StudyEvent : ContextContainer
    {
        private static int _rptKey { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StudyEvent"/> class.
        /// </summary>
        /// <param name="StudyEventOID">The study event oid.</param>
        /// <param name="RepeatKey">The repeat key.</param>
        /// <param name="TransactionType">Type of the transaction.</param>
        /// <param name="InstanceName">Name of the instance.</param>
        /// <param name="InstanceOverdue">The instance overdue.</param>
        /// <param name="InstanceID">The instance identifier.</param>
        public StudyEvent(string StudyEventOID, string RepeatKey, string TransactionType, string InstanceName, string InstanceOverdue, int InstanceID) :
            base(StudyEventOID, RepeatKey, TransactionType)
        {
            this.InstanceName = InstanceName;
            this.InstanceOverdue = InstanceOverdue;
            this.InstanceID = InstanceID;
            this.StudyEventRepeatKey = RepeatKey;
        }

        /// <summary>
        /// Gets the instance identifier.
        /// </summary>
        /// <value>
        /// The instance identifier.
        /// </value>
        public int InstanceID { get; private set; }
        /// <summary>
        /// Gets the name of the instance.
        /// </summary>
        /// <value>
        /// The name of the instance.
        /// </value>
        public string InstanceName { get; private set; }
        /// <summary>
        /// Gets the instance overdue.
        /// </summary>
        /// <value>
        /// The instance overdue.
        /// </value>
        public string InstanceOverdue { get; private set; }
        /// <summary>
        /// Gets the study event repeat key.
        /// </summary>
        /// <value>
        /// The study event repeat key.
        /// </value>
        public string StudyEventRepeatKey { get; private set; }
    }
}
