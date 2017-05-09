using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medidata.RWS.Extras.AuditEvent
{

    /// <summary>
    /// Review related attributes
    /// </summary>
    /// <seealso cref="Medidata.RWS.Extras.AuditEvent.ContextBase" />
    public class Review : ContextBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Review"/> class.
        /// </summary>
        /// <param name="GroupName">Name of the group.</param>
        /// <param name="Reviewed">if set to <c>true</c> [reviewed].</param>
        public Review(string GroupName, bool Reviewed)
        {
            this.GroupName = GroupName;
            this.Reviewed = Reviewed;
        }

        /// <summary>
        /// Gets the name of the group.
        /// </summary>
        /// <value>
        /// The name of the group.
        /// </value>
        public string GroupName { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="Review"/> is reviewed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if reviewed; otherwise, <c>false</c>.
        /// </value>
        public bool Reviewed { get; private set; }
    }
}
