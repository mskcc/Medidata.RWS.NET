using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medidata.RWS.Schema;

namespace Medidata.RWS.Core.DataBuilders
{

    /// <summary>
    /// A builder for constructing "ClinicalData" objects in a state suitable for transmission.
    /// </summary>
    /// <tocexclude />
    public class ClinicalDataBuilder : Builds<List<ODMcomplexTypeDefinitionClinicalData>>
    {

        /// <summary>
        /// The object context to be constructed
        /// </summary>
        private List<ODMcomplexTypeDefinitionClinicalData> clinicalDataList;

        /// <summary>
        /// Represents the first node of the clinicalDataList list.
        /// </summary>
        private ODMcomplexTypeDefinitionClinicalData clinicalData;

        /// <summary>
        /// Initializes a new instance of the ClinicalDataBuilder class using the specified StudyOID value.
        /// </summary>
        /// <param name="StudyOID"></param>
        public ClinicalDataBuilder(string StudyOID)
        {
            
            clinicalData = new ODMcomplexTypeDefinitionClinicalData
            {
                MetaDataVersionOID = "1",
                StudyOID = StudyOID,
                SubjectData = new List<ODMcomplexTypeDefinitionSubjectData>()
            };

            clinicalDataList = new List<ODMcomplexTypeDefinitionClinicalData>
            {
                clinicalData
            };

        }

        /// <summary>
        /// Add a "SubjectKey" node using the specified parameter values.
        /// </summary>
        /// <param name="SubjectKey"></param>
        /// <param name="LocationOID"></param>
        /// <param name="subjectDataBuilder"></param>
        /// <returns></returns>
        public ClinicalDataBuilder WithSubjectData(string SubjectKey, string LocationOID, Action<SubjectDataBuilder> subjectDataBuilder)
        {
            var sdb = new SubjectDataBuilder(SubjectKey, LocationOID);
            subjectDataBuilder(sdb);
            clinicalData.SubjectData.Add(sdb.Build());
            return this;
        }


        /// <summary>
        /// See <see cref="Builds{T}.Build()"/> for more information. 
        /// </summary>
        /// <returns></returns>
        public List<ODMcomplexTypeDefinitionClinicalData> Build()
        {
            return clinicalDataList;
        }

     

    }
}
