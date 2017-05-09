using Medidata.RWS.Schema;

namespace Medidata.RWS.Core.Responses
{
    /// <summary>
    /// Extension of an ODM document, dedicated to parsing error XML from RAVE
    /// </summary>
    public class RwsError : ODM
    {
        /// <summary>
        /// The mdsol namespace value.
        /// </summary>
        const string MEDI_NS = @"http://www.mdsol.com/ns/odm/metadata";

        /// <summary>
        /// The error description.
        /// </summary>
        public readonly string ErrorDescription;


        /// <summary>
        /// Initializes a new instance of the <see cref="RwsError"/> class.
        /// </summary>
        /// <param name="xmlString">The XML string.</param>
        public RwsError(string xmlString) : base(xmlString)
        {
            rootNode = ParseXMLString(xmlString);
            ErrorDescription = rootNode.GetAttribute("ErrorDescription", MEDI_NS);

            var serializedOdm = RWSHelpers.Serializers.XmlDeserializeFromString<ODM>(xmlString);

            CreationDateTime = serializedOdm.CreationDateTime;
            FileOID = serializedOdm.FileOID;
            ODMVersion = serializedOdm.ODMVersion;
            FileType = serializedOdm.FileType;

        }

    }
}