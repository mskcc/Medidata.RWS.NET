using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medidata.RWS.Extras.AuditEvent
{
    /// <summary>
    /// Parser interface for Audit Events.
    /// </summary>
    /// <seealso cref="Medidata.RWS.Extras.IParser" />
    public interface IAuditEventParser : IParser
    {
        /// <summary>
        /// Occurs when [context built].
        /// </summary>
        event ContextBuiltEventHandler ContextBuilt;

        /// <summary>
        /// Gets the last source identifier.
        /// </summary>
        /// <returns>
        /// System.Int32.
        /// </returns>
        int GetLastSourceId();
    }
}
