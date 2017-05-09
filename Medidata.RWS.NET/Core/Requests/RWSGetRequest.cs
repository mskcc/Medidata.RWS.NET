using Medidata.RWS.Core.Responses;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medidata.RWS.Core.Requests
{
    /// <summary>
    /// Represents a GET Request.
    /// </summary>
    /// <seealso cref="Medidata.RWS.Core.Requests.RWSRequest" />
    public abstract class RWSGetRequest : RWSRequest
    {


        /// <summary>
        /// The HTTP method of the request.
        /// </summary>
        public override Method HttpMethod { get { return Method.GET; } }


        /// <summary>
        /// Whether or not the request requires authentication
        /// </summary>
        public override bool RequiresAuthentication { get { return false; } }

        /// <summary>
        /// Default implementation, return the XML string of the response.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public override IRWSResponse Result(IRestResponse response)
        {
            return new RWSResponse(response);
        }
    }


}
