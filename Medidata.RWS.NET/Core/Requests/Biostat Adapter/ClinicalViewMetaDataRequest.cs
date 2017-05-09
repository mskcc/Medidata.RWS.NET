using System.Collections.Generic;

namespace Medidata.RWS.Core.Requests.Biostat_Adapter
{


    /// <summary>
    /// Return Clinical View Metadata as ODM string
    /// </summary>
    /// <seealso cref="Medidata.RWS.Core.Requests.QueryOptionGetRequest" />
    public class ClinicalViewMetaDataRequest : QueryOptionGetRequest
    {

        /// <summary>
        /// Gets the versionitem.
        /// </summary>
        /// <value>
        /// The versionitem.
        /// </value>
        public string versionitem { get; private set; }

        /// <summary>
        /// Gets the codelistsuffix.
        /// </summary>
        /// <value>
        /// The codelistsuffix.
        /// </value>
        public string codelistsuffix { get; private set; }

        /// <summary>
        /// Gets the rawsuffix.
        /// </summary>
        /// <value>
        /// The rawsuffix.
        /// </value>
        public string rawsuffix { get; private set; }

        /// <summary>
        /// The list of query string parameters that can be supplied for this request
        /// </summary>
        public override List<string> KnownQueryOptions
        {
            get
            {
                return new List<string> { "versionitem", "rawsuffix", "codelistsuffix", "decodesuffix" };
            }
        }

        /// <summary>
        /// Gets the name of the project.
        /// </summary>
        /// <value>
        /// The name of the project.
        /// </value>
        public string ProjectName { get; private set; }

        /// <summary>
        /// Gets the name of the environment.
        /// </summary>
        /// <value>
        /// The name of the environment.
        /// </value>
        public string EnvironmentName { get; private set; }

        /// <summary>
        /// Gets the decodesuffix.
        /// </summary>
        /// <value>
        /// The decodesuffix.
        /// </value>
        public string decodesuffix { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClinicalViewMetaDataRequest" /> class.
        /// </summary>
        /// <param name="ProjectName">Name of the project.</param>
        /// <param name="EnvironmentName">Name of the environment.</param>
        /// <param name="versionitem">The versionitem.</param>
        /// <param name="rawsuffix">The rawsuffix.</param>
        /// <param name="codelistsuffix">The codelistsuffix.</param>
        /// <param name="decodesuffix">The decodesuffix.</param>
        public ClinicalViewMetaDataRequest(
            string ProjectName,
            string EnvironmentName,
            string versionitem = default(string),
            string rawsuffix = default(string),
            string codelistsuffix = default(string),
            string decodesuffix = default(string))
        {

            this.ProjectName = ProjectName;
            this.EnvironmentName = EnvironmentName;
            this.versionitem = versionitem;
            this.rawsuffix = rawsuffix;
            this.codelistsuffix = codelistsuffix;
            this.decodesuffix = decodesuffix;

        }


        /// <summary>
        /// The URL path of the resource
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override string UrlPath()
        {
            return RequestHelpers.MakeUrl("/", QueryString(), new string[] { "studies", StudyNameAndEnvironment(), "datasets", "metadata", "regular" });
        }


        /// <summary>
        /// Get the Study and Environment names in a format RWS expects.
        /// </summary>
        /// <returns></returns>
        public string StudyNameAndEnvironment()
        {
            return string.IsNullOrWhiteSpace(EnvironmentName) ? $"{ProjectName}" : $"{ProjectName}({EnvironmentName})";
        }
    }
}
