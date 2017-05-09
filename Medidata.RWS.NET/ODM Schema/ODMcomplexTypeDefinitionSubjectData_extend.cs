using Medidata.RWS.Core;
using Medidata.RWS.Core.DataBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Medidata.RWS.Schema
{

    public partial class ODMcomplexTypeDefinitionSubjectData : Validatable
    {


        public void Validate()
        {
           
        }

        [XmlAttribute(Namespace = Constants.MDSOL_NS, AttributeName = "SubjectName")]
        public string SubjectName { get; set; }

        [XmlAttribute(Namespace = Constants.MDSOL_NS, AttributeName = "SubjectKeyType")]
        public string SubjectKeyType { get; set; }

        [XmlAttribute(Namespace = Constants.MDSOL_NS, AttributeName = "Status")]
        public string Status { get; set; }


    }
}
