using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medidata.RWS.Extras.AuditEvent
{
    /// <summary>
    /// AuditRecord related attributes
    /// </summary>
    public class AuditRecord : ContextBase, HasUserElements
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuditRecord"/> class.
        /// </summary>
        public AuditRecord()
        {
            SourceID = -1;
        }

        /// <summary>
        /// Gets or sets the date time stamp.
        /// </summary>
        /// <value>
        /// The date time stamp.
        /// </value>
        public DateTime DateTimeStamp { get; set; }
        /// <summary>
        /// Gets or sets the location oid.
        /// </summary>
        /// <value>
        /// The location oid.
        /// </value>
        public string LocationOID { get; set; }
        /// <summary>
        /// Gets or sets the reason for change.
        /// </summary>
        /// <value>
        /// The reason for change.
        /// </value>
        public string ReasonForChange { get; set; }
        /// <summary>
        /// Gets or sets the source identifier.
        /// </summary>
        /// <value>
        /// The source identifier.
        /// </value>
        public int SourceID { get; set; }
        /// <summary>
        /// Gets or sets the user oid.
        /// </summary>
        /// <value>
        /// The user oid.
        /// </value>
        public string UserOID { get; set; }

    }
}
