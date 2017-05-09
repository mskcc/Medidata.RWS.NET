using System;
using System.Collections.Generic;

namespace Medidata.RWS.Core.Requests.Datasets
{

    /// <summary>
    /// Return the text of the full datasets listing as an ODM string.
    /// </summary>
    /// <seealso cref="Medidata.RWS.Core.Requests.OdmDatasetBase" />
    public class StudyDatasetRequest : OdmDatasetBase
    {
        /// <summary>
        /// Gets or sets the name of the project.
        /// </summary>
        /// <value>
        /// The name of the project.
        /// </value>
        public string ProjectName { get; set; }
        /// <summary>
        /// Gets or sets the name of the environment.
        /// </summary>
        /// <value>
        /// The name of the environment.
        /// </value>
        public string EnvironmentName { get; set; }
        /// <summary>
        /// Gets or sets the form oid.
        /// </summary>
        /// <value>
        /// The form oid.
        /// </value>
        public string FormOid { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="StudyDatasetRequest"/> class.
        /// </summary>
        /// <param name="project_name">Name of the project.</param>
        /// <param name="environment_name">Name of the environment.</param>
        /// <param name="dataset_type">Type of the dataset.</param>
        /// <param name="formOid">The form oid.</param>
        /// <param name="versionitem">The versionitem.</param>
        /// <param name="rawsuffix">The rawsuffix.</param>
        /// <param name="codelistsuffix">The codelistsuffix.</param>
        /// <param name="decodesuffix">The decodesuffix.</param>
        /// <param name="stdsuffix">The stdsuffix.</param>
        /// <param name="start">The start.</param>
        public StudyDatasetRequest(
            string project_name,
            string environment_name,
            string dataset_type = "regular",
            string formOid = default(string),
            string versionitem = default(string),
            string rawsuffix = default(string),
            string codelistsuffix = default(string),
            string decodesuffix = default(string),
            string stdsuffix = default(string),
            string start = default(string))
            : base(dataset_type, versionitem, rawsuffix, codelistsuffix, decodesuffix, stdsuffix, start)
        {
            ProjectName = project_name;
            EnvironmentName = environment_name;
            FormOid = formOid;
            Verify();
        }

        /// <summary>
        /// Get the Study and Environment names in a format RWS expects.
        /// </summary>
        /// <returns></returns>
        private string StudyNameAndEnvironment()
        {
            return string.IsNullOrWhiteSpace(EnvironmentName) ? $"{ProjectName}" : $"{ProjectName}({EnvironmentName})";
        }

        /// <summary>
        /// The URL path of the resource being requested.
        /// </summary>
        /// <returns></returns>
        public override string UrlPath()
        {

            var queryParams = new List<string> {"studies", StudyNameAndEnvironment(), "datasets", dataset_type};

            if(!string.IsNullOrEmpty(FormOid)) queryParams.Add(FormOid);

            return RequestHelpers.MakeUrl("/", QueryString(), queryParams.ToArray());

        }

    }
}
