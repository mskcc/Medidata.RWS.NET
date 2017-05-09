using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medidata.RWS.Core.Requests.Implementations
{
    /// <summary>
    /// Get a specific version of a study based on the Project Name and MetadataVersion OID.
    /// </summary>
    /// <seealso cref="Medidata.RWS.Core.Requests.Implementations.VersionRequestBase" />
    public class StudyVersionRequest : VersionRequestBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="StudyVersionRequest"/> class.
        /// </summary>
        /// <param name="ProjectName">Name of the project.</param>
        /// <param name="OID">The MetaDataVersion OID.</param>
        public StudyVersionRequest(string ProjectName, string OID) : base(ProjectName, OID)
        {

        }

        /// <summary>
        /// The URL path of the resource being requested.
        /// </summary>
        /// <returns></returns>
        public override string UrlPath()
        {
            return RequestHelpers.MakeUrl("/", new string[] { "metadata", "studies", ProjectName, "versions", OID.ToString() });
        }
    }
}
