using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medidata.RWS.Core.Responses
{
    /// <summary>
    /// Represents a response from PostODMClinicalData messages.
    /// </summary>
    public class RWSPostResponse : RWSResponse
    {
        /// <summary>
        /// The subject number in study
        /// </summary>
        public readonly int SubjectNumberInStudy;
        /// <summary>
        /// The subject number in study site
        /// </summary>
        public readonly int SubjectNumberInStudySite;


        /// <summary>
        /// Initializes a new instance of the <see cref="RWSPostResponse"/> class.
        /// </summary>
        /// <param name="response"></param>
        public RWSPostResponse(IRestResponse response) : base(response)
        {

            SubjectNumberInStudy = rootNode.HasAttribute("SubjectNumberInStudy") ? Convert.ToInt32(rootNode.GetAttribute("SubjectNumberInStudy")) : -1;
            SubjectNumberInStudySite = rootNode.HasAttribute("SubjectNumberInStudySite") ? Convert.ToInt32(rootNode.GetAttribute("SubjectNumberInStudySite")) : -1;

        }
    }
}
