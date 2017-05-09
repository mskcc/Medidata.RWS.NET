using Medidata.RWS.Core.Responses;
using RestSharp;

namespace Medidata.RWS.Core.Requests
{

    /// <summary>
    /// Send a request to flush the RWS cache. Typically used to immediately implement configuration changes in RWS.
    /// </summary>
    public class CacheFlushRequest : RWSAuthorizedGetRequest
    {

        /// <summary>
        /// Default implementation, return the XML string of the response.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public override IRWSResponse Result(IRestResponse response)
        {
            return new RWSResponse(response);
        }

        /// <summary>
        /// The URL path of the resource being requested.
        /// </summary>
        /// <returns></returns>
        public override string UrlPath()
        {
            return RequestHelpers.MakeUrl("/", new string[] { "webservice.aspx?CacheFlush" });
        }


    }
}
