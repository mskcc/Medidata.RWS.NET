using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medidata.RWS.Core.Requests.Biostat_Adapter
{
    /// <summary>
    /// Retrieve data dictionaries from RAVE.
    /// </summary>
    /// <seealso cref="Medidata.RWS.Core.Requests.Biostat_Adapter.FormattedDataSetRequest" />
    public class DataDictionariesRequest : FormattedDataSetRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataDictionariesRequest"/> class.
        /// </summary>
        /// <param name="projectName">Name of the project.</param>
        /// <param name="environmentName">Name of the environment.</param>
        /// <param name="datasetFormat">The dataset format.</param>
        public DataDictionariesRequest(string projectName, string environmentName, string datasetFormat = "csv") : base(projectName, environmentName, datasetFormat)
        {
        }


        public override string UrlPath()
        {
            return RequestHelpers.MakeUrl("/", "datasets", $"{DataSetName()}?studyid={StudyNameAndEnvironment()}");
        }



        /// <summary>
        /// The dataset name.
        /// </summary>
        /// <returns></returns>
        protected override string DataSetName()
        {
            return $"SDTMDataDictionaries{GetFormatExtension(DatasetFormat)}";
        }
    }
}
