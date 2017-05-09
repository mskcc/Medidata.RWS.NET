using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medidata.RWS.Core.Responses;
using RestSharp;

namespace Medidata.RWS.Core.Requests.Implementations
{
    /// <summary>
    /// Base class for study and library metadata version requests
    /// </summary>
    /// <seealso cref="Medidata.RWS.Core.Requests.RWSAuthorizedGetRequest" />
    public abstract class VersionRequestBase : RWSAuthorizedGetRequest
    {
        /// <summary>
        /// The project name
        /// </summary>
        public readonly string ProjectName;
        private readonly string _OID;

        /// <summary>
        /// Initializes a new instance of the <see cref="VersionRequestBase"/> class.
        /// </summary>
        /// <param name="ProjectName">Name of the project.</param>
        /// <param name="OID">The oid.</param>
        public VersionRequestBase(string ProjectName, string OID)
        {
            _OID = OID;
            this.ProjectName = ProjectName;
        }

        /// <summary>
        /// Gets the oid.
        /// </summary>
        /// <value>
        /// The oid.
        /// </value>
        public int OID
        {
            get
            {
                return int.Parse(_OID);
            }

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
