using Medidata.RWS.Core;
using Medidata.RWS.Core.DataBuilders;
using Medidata.RWS.Core.Requests;
using Medidata.RWS.Core.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Medidata.RWS.Schema
{
    public partial class ODM : RWSXMLResponse, Validatable
    {

        public ODM(string xmlString) : base(xmlString)
        {
            //Defaults
            ODMVersion = ODMVersion.Item13;
        }

        public void Validate()
        {
            this.ClinicalData.Validate();
        }

        /// <summary>
        /// Searches for a ODMcomplexTypeDefinitionClinicalData node with the supplied StudyOID.
        /// </summary>
        /// <param name="StudyOID"></param>
        /// <returns></returns>
        public ODMcomplexTypeDefinitionClinicalData FindClinicalDataWithStudyOID(string StudyOID)
        {

            return ClinicalData.Where(x => x.StudyOID == StudyOID).FirstOrDefault();

        }

    }
}
