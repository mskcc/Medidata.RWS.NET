using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medidata.RWS.Core.Requests;

namespace Medidata.RWS.Core.Requests.Datasets
{
    public class VersionDatasetRequest : OdmDatasetBase
    {
        public VersionDatasetRequest(

            string project_name,
            string environment_name,
            string version_oid,
            string dataset_type = "regular",
            string formOid = default(string),
            string versionitem = default(string),
            string rawsuffix = default(string),
            string codelistsuffix = default(string),
            string decodesuffix = default(string),
            string stdsuffix = default(string),
            string start = default(string)) : base(dataset_type, versionitem, rawsuffix, codelistsuffix, decodesuffix, stdsuffix, start)
        {

            ProjectName = project_name;
            EnvironmentName = environment_name;
            VersionOid = version_oid;
            FormOid = formOid;
            Verify();
        }

        public string VersionOid { get; set; }

        public string FormOid { get; set; }

        public string EnvironmentName { get; set; }

        public string ProjectName { get; set; }

        /// <summary>
        /// Get the Study and Environment names in a format RWS expects.
        /// If no environment name is provided, it is left out of the return string.
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

            var queryParams = new List<string> { "studies", StudyNameAndEnvironment(), "versions", VersionOid, "datasets", dataset_type };

            if (!string.IsNullOrEmpty(FormOid)) queryParams.Add(FormOid);

            return RequestHelpers.MakeUrl("/", QueryString(), queryParams.ToArray());

        }
    }
}
