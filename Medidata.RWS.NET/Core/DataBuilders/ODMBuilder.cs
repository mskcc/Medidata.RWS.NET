using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using Medidata.RWS.Schema;

namespace Medidata.RWS.Core.DataBuilders
{
    /// <summary>
    /// A builder for constructing "ODM" objects in a state suitable for transmission.
    /// </summary>
    /// <tocexclude />
    public class ODMBuilder : Builds<ODM>, Validatable
    {

        /// <summary>
        /// The object to be built.
        /// </summary>
        private ODM odm;

        /// <summary>
        /// Initializes a new instance of the ODMBuilder class.
        /// </summary>
        public ODMBuilder()
        {
            odm = new ODM
            {

                FileOID = Guid.NewGuid().ToString(),
                FileType = FileType.Transactional,
                ODMVersion = ODMVersion.Item13,
                CreationDateTime = DateTime.Now,
                ClinicalData = new List<ODMcomplexTypeDefinitionClinicalData>()
            };
        }

        /// <summary>
        /// Initialize a new instance of the ODMBuilder class, using the supplied ODM object as the 
        /// object context.
        /// </summary>
        /// <param name="_odm"></param>
        public ODMBuilder(ODM _odm)
        {
            odm = _odm;
        }

        /// <summary>
        /// See <see cref="Builds{T}.Build()"></see> for more information.
        /// </summary>
        /// <returns></returns>
        public ODM Build()
        {
            return odm;
        }


        /// <summary>
        /// Add a "ClinicalData" node using the specified StudyOID value. 
        /// Returns the current builder instance.
        /// </summary>
        /// <param name="StudyOID"></param>
        /// <param name="clinicalDataBuilder"></param>
        /// <returns></returns>
        public ODMBuilder WithClinicalData(string StudyOID, Action<ClinicalDataBuilder> clinicalDataBuilder)
        {
            var cdb = new ClinicalDataBuilder(StudyOID);
            clinicalDataBuilder(cdb);
            odm.ClinicalData = cdb.Build();
            return this;
        }


        /// <summary>
        /// Serialize the ODM object as XML and return a string representation.
        /// </summary>
        /// <returns></returns>
        public string AsXMLString()
        {

            var xmlNameSpace = new XmlSerializerNamespaces();
            xmlNameSpace.Add("mdsol", Constants.MDSOL_NS);
            var serializer = new XmlSerializer(odm.GetType());
            string serialized;
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, odm, xmlNameSpace);
                serialized = writer.ToString();
            }
            return serialized;
        }


        /// <summary>
        /// Return an XmlDocument representation of the XML.
        /// </summary>
        /// <returns></returns>
        public XDocument AsXDocument()
        {

            return XDocument.Parse(AsXMLString());

        }

        /// <summary>
        /// Validate the entire ODM structure.
        /// </summary>
        public void Validate()
        {

            odm.Validate();
            
        }

    }
}
