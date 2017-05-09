using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medidata.RWS.Core.Requests.ODM_Adapter
{
    /// <summary>
    /// Admin data for all sites in the study can be retrieved from the Sites pre-installed custom dataset.
    /// </summary>
    /// <seealso cref="Medidata.RWS.Core.Requests.QueryOptionGetRequest" />
    public class SitesRequest : QueryOptionGetRequest
    {
        /// <summary>
        /// The project name
        /// </summary>
        public readonly string ProjectName;

        /// <summary>
        /// The environment
        /// </summary>
        public readonly string Environment;


        /// <summary>
        /// Initializes a new instance of the <see cref="SitesRequest"/> class.
        /// </summary>
        /// <param name="ProjectName">Name of the project.</param>
        /// <param name="Environment">The environment.</param>
        public SitesRequest(string ProjectName, string Environment)
        {



            if(string.IsNullOrEmpty(ProjectName) && !string.IsNullOrEmpty(Environment))
            {
                throw new ArgumentException(
                    string.Format("ProjectName cannot be blank if Environment is supplied."));
            }

            if (!string.IsNullOrEmpty(ProjectName) && string.IsNullOrEmpty(Environment))
            {
                throw new ArgumentException(
                    string.Format("Environment cannot be blank if ProjectName is supplied."));
            }

            this.ProjectName = ProjectName;
            this.Environment = Environment;



        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SitesRequest"/> class. 
        /// </summary>
        public SitesRequest()
        {
        }


        /// <summary>
        /// The study name and environment, for example: "Mediflex(Prod)"
        /// </summary>
        public string studyoid
        {
            get
            {
                if (string.IsNullOrEmpty(ProjectName) && string.IsNullOrEmpty(Environment)) return "";

                return string.IsNullOrWhiteSpace(Environment) ? $"{ProjectName}" : $"{ProjectName}({Environment})";

            }

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
            return RequestHelpers.MakeUrl("/", QueryString(), new string[] { "datasets", "Sites.odm" });
        }
    }
}
