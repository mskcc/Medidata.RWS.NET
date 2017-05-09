using Medidata.RWS.Core.Responses;
using RestSharp;

namespace Medidata.RWS.Core.Requests
{
    /// <summary>
    /// Get the RWS version number
    /// </summary>
    /// <seealso cref="Medidata.RWS.Core.Requests.RWSGetRequest" />
    public class VersionRequest : RWSGetRequest
    {
        /// <summary>
        /// The URL path of the resource being requested.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override string UrlPath()
        {
            return RequestHelpers.MakeUrl("/", "version");
        }

        /// <summary>
        /// Default implementation, return a text representation of the response.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public override IRWSResponse Result(IRestResponse response)
        {
            return new RWSTextResponse(response.Content);
        }
    }


}
