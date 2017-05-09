using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Medidata.RWS.Schema
{
    public partial class ODMcomplexTypeDefinitionStudy
    {

        [XmlAttribute(Namespace = "http://www.mdsol.com/ns/odm/metadata", AttributeName = "ProjectType")]
        public string ProjectType
        {
            get; set;
        }

    }
}
