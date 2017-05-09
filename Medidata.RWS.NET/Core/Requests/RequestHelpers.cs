using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace Medidata.RWS.Core.Requests
{
    /// <summary>
    /// Helpers for RWS Requests.
    /// </summary>
    public static class RequestHelpers
    {

        /// <summary>
        /// Create a url out of multiple parameters and a separator.
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="uriSegments"></param>
        /// <returns></returns>
        public static string MakeUrl(string separator = "/", params string[] uriSegments)
        {
            string result = string.Empty;
            for (int i = 0; i < uriSegments.Length; i++)
            {
                if (i > 0)
                    result += separator;

                result += uriSegments[i].Contains("?") ||
                    uriSegments[i].Contains(" ") ? uriSegments[i] : HttpUtility.UrlEncode(uriSegments[i]);

            }

            return result;

        }

        /// <summary>
        /// Create a url with multiple parameters, a separator, and a list of query string key/value pairs.
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="queryStringParams"></param>
        /// <param name="uriSegments"></param>
        /// <returns></returns>
        public static string MakeUrl(string separator = "/", Dictionary<string, string> queryStringParams = null, params string[] uriSegments)
        {
            var result = MakeUrl(separator, uriSegments);

            if (queryStringParams != null && queryStringParams.Count > 0)
            {
                return string.Format("{0}?{1}", result, ToQueryString(queryStringParams));
            }

            return result;
        }


        /// <summary>
        /// Create a query string out of a list of parameters.
        /// </summary>
        /// <param name="paramList"></param>
        /// <returns></returns>
        private static string ToQueryString(Dictionary<string, string> paramList)
        {
            NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);

            foreach (var qpm in paramList)
            {
                queryString.Add(qpm.Key, qpm.Value);
            }

            return queryString.ToString();


        }
    }
}
