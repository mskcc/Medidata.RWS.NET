using Medidata.RWS.Core.Responses;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medidata.RWS.Core.Requests.Implementations
{
    /// <summary>
    /// Post data to RWS.
    /// </summary>
    /// <seealso cref="Medidata.RWS.Core.Requests.RWSAuthorizedPostRequest" />
    public class PostDataRequest : RWSAuthorizedPostRequest
    {




        /// <summary>
        /// The data to be posted.
        /// </summary>
        public string Data { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="PostDataRequest"/> class.
        /// </summary>
        /// <param name="data">The data to be posted</param>
        public PostDataRequest(string data)
        {
            this.Data = data;
            this.RequestBody = data;

        }

        /// <summary>
        /// Gets the HTTP headers.
        /// </summary>
        /// <value>
        /// The headers.
        /// </value>
        public override Dictionary<string, string> Headers
        {
            get
            {
                return new Dictionary<string, string>
                {
                    { "Content-Type", "text/xml"}
                };

            }
        }

        /// <summary>
        /// The URL path of the resource being requested.
        /// </summary>
        /// <returns></returns>
        public override string UrlPath()
        {
            return RequestHelpers.MakeUrl("/", new string[] { "webservice.aspx?PostODMClinicalData" });
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
