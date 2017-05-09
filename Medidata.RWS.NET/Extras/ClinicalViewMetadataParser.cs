using System.Linq;
using Medidata.RWS.Core.Responses;
using System.Xml.Linq;

namespace Medidata.RWS.Extras
{
    /// <summary>
    /// Parser for Clinical View Metadata ODM responses.
    /// </summary>
    /// <seealso cref="Medidata.RWS.Extras.IParser" />
    public class ClinicalViewMetadataParser : AbstractParser
    {


        // ===========
        // Elements
        // ===========
        /// <summary>
        /// The "FormDef" element.
        /// </summary>
        public XName FormDef = Odm("FormDef");

        /// <summary>
        /// The "ItemDef" element
        /// </summary>
        public XName ItemDef = Odm("ItemDef");

        // ===========
        // Attributes
        // ===========
        /// <summary>
        /// The "OID" Attribute
        /// </summary>
        public XName OID = "OID";

        /// <summary>
        /// The "ItemOID" Attribute
        /// </summary>
        public XName ItemOID = "ItemOID";

        /// <summary>
        /// Initializes a new instance of the <see cref="ClinicalViewMetadataParser"/> class.
        /// </summary>
        /// <param name="odmResponse">The odm response.</param>
        public ClinicalViewMetadataParser(IRWSResponse odmResponse)
        {

            SetResponse(odmResponse);

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClinicalViewMetadataParser"/> class.
        /// </summary>
        public ClinicalViewMetadataParser()
        {
        }

        /// <summary>
        /// Starts this parser instance.
        /// </summary>
        public override void Start()
        {

        }

        /// <summary>
        /// Finds an element with a given attribute value.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="attribute">The attribute.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public bool FindElementWithAttributeValue(string element, string attribute, string value)
        {
            if (GetType().GetField(element) == null || GetType().GetField(attribute) == null) return false;
            var ELEMENT = GetType().GetField(element).GetValue(this);

            var ATTRIBUTE = GetType().GetField(attribute).GetValue(this);

            if (string.IsNullOrEmpty(ELEMENT?.ToString())) return false;

            if (string.IsNullOrEmpty(ATTRIBUTE?.ToString())) return false;

            var res = OdmXmlDoc.Descendants(ELEMENT.ToString())
                .FirstOrDefault(el => el.Attribute(ATTRIBUTE.ToString()) != null &&
                                      el.Attribute(ATTRIBUTE.ToString())?.Value == value);

            return res != null;
        }
    }
}
