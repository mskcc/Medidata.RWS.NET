using System.Collections.Generic;

namespace Medidata.RWS.Core.Requests
{

    /// <summary>
    /// Manages requests that have known query string options
    /// </summary>
    public abstract class QueryOptionGetRequest : RWSAuthorizedGetRequest
    {

        /// <summary>
        /// The list of query string parameters that can be supplied for this request, to be defined in
        /// derived classes.
        /// </summary>
        public abstract List<string> KnownQueryOptions { get; }


        /// <summary>
        /// Create a new key value list of query string parameters.
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<string, string> QueryString()
        {

            var parameters = new Dictionary<string, string>();

            foreach (var key in KnownQueryOptions)
            {
                if (GetType().GetProperty(key) == null) continue;
                var propVal = GetType().GetProperty(key).GetValue(this, null);
                if (string.IsNullOrEmpty(propVal?.ToString())) continue;
                parameters.Add(key, propVal.ToString());
            }

            return parameters;

        }


    }
}
