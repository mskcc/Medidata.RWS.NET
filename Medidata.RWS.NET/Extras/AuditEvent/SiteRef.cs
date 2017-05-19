using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medidata.RWS.Extras.AuditEvent
{
    /// <summary>
    /// SiteRef related attributes.
    /// </summary>
    /// <seealso cref="Medidata.RWS.Extras.AuditEvent.ContextBase" />
    public class SiteRef : ContextBase
    {

        /// <summary>
        /// Gets or sets the location oid.
        /// </summary>
        /// <value>
        /// The location oid.
        /// </value>
        public string LocationOID { get; set; }
    }
}
