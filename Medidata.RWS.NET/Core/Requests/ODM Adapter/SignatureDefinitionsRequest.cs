using System;
using System.Collections.Generic;
using Medidata.RWS.Core.Requests;

namespace Medidata.RWS
{
    /// <summary>
    /// Return signature definitions for all versions of a study.
    /// </summary>
    /// <seealso cref="Medidata.RWS.Core.Requests.QueryOptionGetRequest" />
    public class SignatureDefinitionsRequest : QueryOptionGetRequest
    {
        /// <summary>
        /// Gets the name of the project.
        /// </summary>
        /// <value>
        /// The name of the project.
        /// </value>
        public string ProjectName { get; private set; }

        /// <summary>
        /// Gets the studyid. This parameter is used as part of the query string.
        /// </summary>
        /// <value>
        /// The studyid.
        /// </value>
        public string studyid
        {
            get { return ProjectName; }
        }

        /// <summary>
        /// The list of query string parameters that can be supplied for this request
        /// </summary>
        public override List<string> KnownQueryOptions
        {
            get
            {
                return new List<string> { "studyid" };
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SignatureDefinitionsRequest"/> class.
        /// </summary>
        /// <param name="ProjectName">Name of the project.</param>
        public SignatureDefinitionsRequest(string ProjectName)
        {
            this.ProjectName = ProjectName;
        }

        /// <summary>
        /// The URL path of the resource being requested.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override string UrlPath()
        {
            return RequestHelpers.MakeUrl("/", QueryString(), new string[] { "datasets", "Signatures.odm" });
        }
    }
}