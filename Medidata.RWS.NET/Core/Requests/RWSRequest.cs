using Medidata.RWS.Core.Responses;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Medidata.RWS.Core.Requests
{

    /// <summary>
    /// Base class for RWS Requests
    /// </summary>
    /// <seealso cref="Medidata.RWS.Core.Requests.IRWSRequest" />
    public abstract class RWSRequest : IRWSRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RWSRequest"/> class.
        /// </summary>
        public RWSRequest()
        {
            Headers = new Dictionary<string, string>();
        }

        /// <summary>
        /// The HTTP method of the request.
        /// </summary>
        public abstract Method HttpMethod { get; }

        /// <summary>
        /// Whether or not the request requires authentication
        /// </summary>
        public abstract bool RequiresAuthentication { get; }

        /// <summary>
        /// The URL path of the resource being requested.
        /// </summary>
        /// <returns></returns>
        public abstract string UrlPath();

        /// <summary>
        /// The result of the request.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public abstract IRWSResponse Result(IRestResponse response);

        /// <summary>
        /// Gets a list of the HTTP headers.
        /// </summary>
        /// <value>
        /// The HTTP headers.
        /// </value>
        public virtual Dictionary<string, string> Headers { get; }

        /// <summary>
        /// Gets the request body.
        /// </summary>
        /// <value>
        /// The request body.
        /// </value>
        public string RequestBody { get; protected set; }
    }
    
}
