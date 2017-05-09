using Medidata.RWS.Core.Responses;
using Medidata.RWS.Core.RWSObjects;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medidata.RWS.Core.Requests.Implementations
{
    /// <summary>
    /// Requests a list of active study CRF versions accessible to the user.
    /// </summary>
    public class StudyVersionsRequest : RWSAuthorizedGetRequest
    {
        private readonly string ProjectName;


        /// <summary>
        /// Initializes a new instance of the <see cref="StudyVersionsRequest"/> class.
        /// </summary>
        /// <param name="ProjectName">Name of the project.</param>
        public StudyVersionsRequest(string ProjectName)
        {
            this.ProjectName = ProjectName;
        }


        /// <summary>
        /// Return a <see cref="RWSStudyMetadataVersions"/> object based on the response from RWS.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public override IRWSResponse Result(IRestResponse response)
        {

            return new RWSStudyMetadataVersions(response.Content);

        }


        /// <summary>
        /// The URL path of the resource being requested.
        /// </summary>
        /// <returns></returns>
        public override string UrlPath()
        {
            return RequestHelpers.MakeUrl("/", new string[] { "metadata", "studies", ProjectName, "versions" });
        }
    }
}
