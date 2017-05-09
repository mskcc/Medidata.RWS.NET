using Medidata.RWS.Core.Responses;
using RestSharp;

namespace Medidata.RWS.Core.Requests
{
    /// <summary>
    /// Request the RWS build version number.
    /// </summary>
    public class BuildVersionRequest : RWSGetRequest
    {

        /// <summary>
        /// Default implementation, return a text representation of the response.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public override IRWSResponse Result(IRestResponse response)
        {
            return new RWSTextResponse(response.Content);
        }


        /// <summary>
        /// The URL path of the resource being requested.
        /// </summary>
        /// <returns></returns>
        public override string UrlPath()
        {
            return RequestHelpers.MakeUrl("/", new string[] { "version", "build" });
        }
    }
}
