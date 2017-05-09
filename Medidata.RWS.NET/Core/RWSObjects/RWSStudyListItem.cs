using Medidata.RWS.Core.Responses;
using Medidata.RWS.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medidata.RWS.Core.RWSObjects
{
    /// <summary>
    /// Represents a single study.
    /// </summary>
    public class RWSStudyListItem
    {

        /// <summary>
        /// The oid
        /// </summary>
        public readonly string OID;
        /// <summary>
        /// The study name
        /// </summary>
        public readonly string StudyName;
        /// <summary>
        /// The protocol name
        /// </summary>
        public readonly string ProtocolName;
        /// <summary>
        /// The environment
        /// </summary>
        public readonly string Environment;
        /// <summary>
        /// The project type
        /// </summary>
        public readonly string ProjectType;

        /// <summary>
        /// Initializes a new instance of the <see cref="RWSStudyListItem"/> class.
        /// </summary>
        public RWSStudyListItem()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RWSStudyListItem"/> class.
        /// </summary>
        /// <param name="study">The study.</param>
        public RWSStudyListItem(ODMcomplexTypeDefinitionStudy study)
        {
            OID = study.OID;
            StudyName = study.GlobalVariables.StudyName.Value;
            ProtocolName = study.GlobalVariables.ProtocolName.Value;
            ProjectType = study.ProjectType;
            Environment = RWSHelpers.Helpers.GetEnvironmentFromStudyNameAndProtocol(StudyName, ProtocolName);

        }



        /// <summary>
        /// Whether or not this study is a production study.
        /// </summary>
        public bool IsProduction
        {
            get
            {
                return Environment == "" && ProjectType != "GlobalLibraryVolume";
            }
        }
    }
}
