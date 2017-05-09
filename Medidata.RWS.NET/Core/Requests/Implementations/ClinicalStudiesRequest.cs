using Medidata.RWS.Core.Responses;
using Medidata.RWS.Core.RWSObjects;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medidata.RWS.Core.Requests.Implementations
{
    /// <summary>
    ///  Request a list of your EDC studies. Excludes studies you are not associated with.
    /// </summary>
    public class ClinicalStudiesRequest : RWSAuthorizedGetRequest
    {

        /// <summary>
        /// Return a <see cref="RWSStudies"/> object based on the response from RWS.
        /// </summary>
        /// <param name="response"></param>
        /// <returns> </returns>
        public override IRWSResponse Result(IRestResponse response)
        {

            return new RWSStudies(response.Content);

        }

        /// <summary>
        /// The URL path of the resource being requested.
        /// </summary>
        /// <returns></returns>
        public override string UrlPath()
        {
            return RequestHelpers.MakeUrl("/", new string[] { "studies" });
        }
    }
}
