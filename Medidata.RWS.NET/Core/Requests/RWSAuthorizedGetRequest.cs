using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medidata.RWS.Core.Requests
{
    /// <summary>
    /// Represents a GET request that requires authentication.
    /// </summary>
    /// <seealso cref="Medidata.RWS.Core.Requests.RWSGetRequest" />
    public abstract class RWSAuthorizedGetRequest : RWSGetRequest
    {
        /// <summary>
        /// Whether or not the request requires authentication
        /// </summary>
        public override bool RequiresAuthentication { get { return true; } }

    }
}
