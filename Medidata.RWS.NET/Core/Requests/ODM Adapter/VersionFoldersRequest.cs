using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medidata.RWS.Core.Requests.ODM_Adapter
{


    /// <summary>
    /// All folders in use in a study.
    /// </summary>
    /// <seealso cref="Medidata.RWS.Core.Requests.QueryOptionGetRequest" />
    public class VersionFoldersRequest : QueryOptionGetRequest
    {
        private readonly string Environment;
        private readonly string ProjectName;

        /// <summary>
        /// The study name and environment, for example: "Mediflex(Prod)"
        /// </summary>
        public string studyoid => string.IsNullOrWhiteSpace(Environment) ? $"{ProjectName}" : $"{ProjectName}({Environment})";


        /// <summary>
        /// Initializes a new instance of the <see cref="VersionFoldersRequest"/> class.
        /// </summary>
        /// <param name="ProjectName">Name of the project.</param>
        /// <param name="Environment">The environment.</param>
        public VersionFoldersRequest(string ProjectName, string Environment)
        {
            this.ProjectName = ProjectName;
            this.Environment = Environment;
        }

        /// <summary>
        /// The list of query string parameters that can be supplied for this request.
        /// </summary>
        public override List<string> KnownQueryOptions
        {
            get
            {
                return new List<string> { "studyoid" };
            }
        }

        /// <summary>
        /// The request URL path.
        /// </summary>
        /// <returns></returns>
        public override string UrlPath()
        {
            return RequestHelpers.MakeUrl("/", QueryString(), new string[] { "datasets", "VersionFolders.odm" });
        }
    }
}
