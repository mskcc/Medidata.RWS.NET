using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medidata.RWS.Core.Requests.ODM_Adapter
{

    /// <summary>
    /// Request class that provides access to the Rave EDC audit trail. 
    /// See https://learn.mdsol.com/api/rws/retrieving-clinical-data-with-the-clinical-audit-records-dataset-in-rws-53356712.html
    /// </summary>
    public class AuditRecordsRequest : QueryOptionGetRequest
    {
        private readonly string ProjectName;
        private readonly string Environment;

        /// <summary>
        /// The audit ID to start from.
        /// </summary>
        public int? startid { get; private set; }

        /// <summary>
        /// The amount of audits to consider for export.
        /// </summary>
        public int per_page { get; private set; }


        /// <summary>
        /// The study name and environment, for example: "Mediflex(Prod)"
        /// </summary>
        public string studyoid => string.IsNullOrWhiteSpace(Environment) ? $"{ProjectName}" : $"{ProjectName}({Environment})";

        /// <summary>
        /// The list of query string parameters that can be supplied for this request.
        /// </summary>
        public override List<string> KnownQueryOptions
        {
            get
            {
                return new List<string> { "studyoid", "startid", "per_page" };
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditRecordsRequest"/> class.
        /// </summary>
        /// <param name="ProjectName">Name of the project.</param>
        /// <param name="Environment">The environment.</param>
        /// <param name="startid">The startid.</param>
        /// <param name="per_page">The per page value.</param>
        public AuditRecordsRequest(string ProjectName, string Environment, int? startid=1, int per_page=100)
        {
            this.ProjectName = ProjectName;
            this.Environment = Environment;
            this.startid = startid;
            this.per_page = per_page;
        }

        /// <summary>
        /// The URL path of the resource being requested.
        /// </summary>
        /// <returns></returns>
        public override string UrlPath()
        {
            return RequestHelpers.MakeUrl("/", QueryString(), new string[] { "datasets", "ClinicalAuditRecords.odm" });
        }
    }
}
