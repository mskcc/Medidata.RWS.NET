using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medidata.RWS.Core.Responses;
using RestSharp;

namespace Medidata.RWS.Core.Requests
{
    /// <summary>
    /// Represents a POST Request.
    /// </summary>
    /// <seealso cref="Medidata.RWS.Core.Requests.RWSRequest" />
    public abstract class RWSPostRequest : RWSRequest
    {
        /// <summary>
        /// The HTTP method of the request.
        /// </summary>
        public override Method HttpMethod
        {
            get
            {
                return Method.POST;
            }
        }

        /// <summary>
        /// Whether or not the request requires authentication
        /// </summary>
        public override bool RequiresAuthentication
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Return a <see cref="RWSPostResponse"/> object based on the response from RWS.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public override IRWSResponse Result(IRestResponse response)
        {
            return new RWSPostResponse(response);
        }


    }
}
