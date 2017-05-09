using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medidata.RWS.Extras.AuditEvent
{

    /// <summary>
    /// Signature related attributes
    /// </summary>
    /// <seealso cref="Medidata.RWS.Extras.AuditEvent.ContextBase" />
    /// <seealso cref="Medidata.RWS.Extras.AuditEvent.HasUserElements" />
    public class Signature : ContextBase, HasUserElements
    {
        /// <summary>
        /// Gets or sets the date time stamp.
        /// </summary>
        /// <value>
        /// The date time stamp.
        /// </value>
        public DateTime DateTimeStamp { get; set; }
        /// <summary>
        /// Gets or sets the oid.
        /// </summary>
        /// <value>
        /// The oid.
        /// </value>
        public string OID { get; set; }
        /// <summary>
        /// Gets or sets the user oid.
        /// </summary>
        /// <value>
        /// The user oid.
        /// </value>
        public string UserOID { get; set; }
        /// <summary>
        /// Gets or sets the location oid.
        /// </summary>
        /// <value>
        /// The location oid.
        /// </value>
        public string LocationOID { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Signature"/> class.
        /// </summary>
        public Signature()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Signature"/> class.
        /// </summary>
        /// <param name="OID">The oid.</param>
        /// <param name="UserOID">The user oid.</param>
        /// <param name="LocationOID">The location oid.</param>
        /// <param name="DateTimeStamp">The date time stamp.</param>
        public Signature(string OID, string UserOID, string LocationOID, DateTime DateTimeStamp)
        {
            this.OID = OID;
            this.UserOID = UserOID;
            this.LocationOID = LocationOID;
            this.DateTimeStamp = DateTimeStamp;
        }
    }
}
