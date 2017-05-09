using Medidata.RWS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medidata.RWS.Schema;

namespace Medidata.RWS
{
    /// <summary>
    /// Extension methods for ODM classes.
    /// </summary>
    /// <exclude></exclude>
    public static class Extensions
    {
        /// <summary>
        /// Validate a list of ODMcomplexTypeDefinitionClinicalData.
        /// </summary>
        /// <param name="clinicalDataList"></param>
        public static void Validate(this List<ODMcomplexTypeDefinitionClinicalData> clinicalDataList)
        {
            foreach(var cData in clinicalDataList)
            {
                cData.Validate();
            }

        }



        /// <summary>
        /// Validate a list of ODMcomplexTypeDefinitionSubjectData.
        /// </summary>
        /// <param name="subjectDataList"></param>
        public static void Validate(this List<ODMcomplexTypeDefinitionSubjectData> subjectDataList)
        {
            foreach (var sData in subjectDataList)
            {
                sData.Validate();
            }

        }

    }

}
