using Medidata.RWS.Core;
using Medidata.RWS.Core.DataBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Medidata.RWS.Schema
{
    /// <summary>
    /// ClinicalData ODM node.
    /// </summary>
    public partial class ODMcomplexTypeDefinitionClinicalData : Validatable
    {
        /// <summary>
        /// Validate ClinicalData state
        /// </summary>
        public void Validate()
        {
            SubjectData.Validate();
        }



        [XmlAttribute(Namespace = Constants.MDSOL_NS, AttributeName = "AuditSubCategoryName")]
        public string AuditSubCategoryName { get; set; }


    }
}
