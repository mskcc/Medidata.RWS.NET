using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medidata.RWS.Schema;

namespace Medidata.RWS.Core.RWSObjects
{
    /// <summary>
    /// Represents a single subject
    /// </summary>
    public class RWSSubjectListItem
    {
        /// <summary>
        /// The metadata version oid
        /// </summary>
        public readonly string MetadataVersionOID;
        /// <summary>
        /// The study oid
        /// </summary>
        public readonly string StudyOID;
        /// <summary>
        /// The subject key
        /// </summary>
        public readonly string SubjectKey;
        private readonly string LocationOID;
        private readonly ODMcomplexTypeDefinitionSiteRef _SiteRef;
        private readonly ODMcomplexTypeDefinitionSubjectData _SubjectData;

        /// <summary>
        /// Initializes a new instance of the <see cref="RWSSubjectListItem"/> class.
        /// </summary>
        /// <param name="clinData">The clinical data.</param>
        public RWSSubjectListItem(ODMcomplexTypeDefinitionClinicalData clinData)
        {

            StudyOID = clinData.StudyOID;
            MetadataVersionOID = clinData.MetaDataVersionOID;
            _SubjectData = clinData.SubjectData.First();
            SubjectKey = _SubjectData.SubjectKey;
            _SiteRef = _SubjectData.SiteRef;
            LocationOID = _SiteRef.LocationOID;
        }

        /// <summary>
        /// Get the subject name - if the SubjectKeyType is SubjectUUID
        /// then the subject name lives in the mdsol:SubjectName attribute. Otherwise, just return SubjectKey.
        /// </summary>
        public string SubjectName
        {
            get
            {

                if (_SubjectData.SubjectKeyType != null && _SubjectData.SubjectKeyType.ToLower() == "SubjectUUID".ToLower())
                {
                    return _SubjectData.SubjectName;
                }
                
                return SubjectKey;
                
            }
        }

    }
}
