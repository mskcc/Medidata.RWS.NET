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
    /// Request a listing of all the subjects in a study, optionally including those currently inactive or deleted.
    /// </summary>
    public class StudySubjectsRequest : RWSAuthorizedGetRequest
    {

        /// <summary>
        /// The subject key types
        /// </summary>
        public readonly List<string> SUBJECT_KEY_TYPES = new List<string> { "SubjectName", "SubjectUUID" };
        
        /// <summary>
        /// The include options
        /// </summary>
        public readonly List<string> INCLUDE_OPTIONS = new List<string> { "inactive", "deleted", "inactiveAndDeleted" };

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
        /// Gets a value indicating whether this <see cref="StudySubjectsRequest"/> should include status.
        /// </summary>
        /// <value>
        ///   <c>true</c> if yes; otherwise, <c>false</c>.
        /// </value>
        public bool Status { get; private set; }


        /// <summary>
        /// Gets a value indicating whether this <see cref="StudySubjectsRequest"/> should include links.
        /// </summary>
        /// <value>
        ///   <c>true</c> if yes; otherwise, <c>false</c>.
        /// </value>
        public bool Links { get; private set; }

        /// <summary>
        /// Gets the include parameter.
        /// </summary>
        /// <value>
        /// See <see cref="INCLUDE_OPTIONS"/> for allowable values.
        /// </value>
        public string Include { get; private set; }


        /// <summary>
        /// Gets the type of the subject key.
        /// </summary>
        /// <value>
        /// The type of the subject key.
        /// </value>
        public string SubjectKeyType { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="StudySubjectsRequest"/> class.
        /// </summary>
        /// <param name="projectName">Name of the project.</param>
        /// <param name="environmentName">Name of the environment.</param>
        /// <param name="include">The include.</param>
        /// <param name="status">if set to <c>true</c>, include [status].</param>
        /// <param name="subjectKeyType">Type of the subject key.</param>
        /// <param name="links">if set to <c>true</c>, include [links].</param>
        /// <exception cref="System.NotSupportedException">
        /// </exception>
        public StudySubjectsRequest(
            string projectName, 
            string environmentName,
            string include = default(string),
            bool status = false, 
            string subjectKeyType="SubjectName",
            bool links = false)
        {

            ProjectName = projectName;
            EnvironmentName = environmentName;
            Status = status;
            Links = links;
            SubjectKeyType = subjectKeyType;

            if (!SUBJECT_KEY_TYPES.Contains(SubjectKeyType)) throw new NotSupportedException(string.Format("SubjectKeyType {0} is not a valid value", SubjectKeyType));

            if(include != null)
            {
                if (!INCLUDE_OPTIONS.Contains(include)) throw new NotSupportedException(string.Format("If provided, `include` must be one of the following: {0}", string.Join(",", INCLUDE_OPTIONS)));
            }

            Include = include;
        }

        /// <summary>
        /// The URL path of the resource being requested.
        /// </summary>
        /// <returns></returns>
        public override string UrlPath()
        {
            return RequestHelpers.MakeUrl("/", QueryString(), new string[] { "studies", StudyNameAndEnvironment(), "subjects" });
        }


        /// <summary>
        /// Construct the query string.
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> QueryString()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            if(Status)
            {
                parameters.Add("status", "all");
            }

            if(Links)
            {
                parameters.Add("links", "all");
            }

            if(Include != null)
            {
                parameters.Add("include", Include);
            }

            if(SubjectKeyType != "SubjectName")
            {
                parameters.Add("subjectKeyType", SubjectKeyType);
            }

            return parameters;
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
        /// Return a <see cref="RWSSubjects"/> object based on the response from RWS.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public override IRWSResponse Result(IRestResponse response)
        {

            return new RWSSubjects(response.Content);

        }

    }
}
