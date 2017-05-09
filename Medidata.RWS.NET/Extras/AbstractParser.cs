using Medidata.RWS.Core.Responses;
using System.Xml.Linq;

namespace Medidata.RWS.Extras
{
    /// <summary>
    /// An abstract parser class used to perform actions on XDocument nodes.
    /// </summary>
    /// <seealso cref="Medidata.RWS.Extras.IParser" />
    public abstract class AbstractParser : IParser
    {

        /// <summary>
        /// Gets the odm XML document.
        /// </summary>
        /// <value>
        /// The odm XML document.
        /// </value>
        public XDocument OdmXmlDoc { get; private set; }


        /// <summary>
        /// Fluent method to set the odm response.
        /// </summary>
        /// <param name="odmResponse">The odm response.</param>
        /// <returns></returns>
        public IParser ForOdmResponse(IRWSResponse odmResponse)
        {
            SetResponse(odmResponse);

            return this;
        }

        /// <summary>
        /// Sets the response.
        /// </summary>
        /// <param name="odmResponse">The odm response.</param>
        public void SetResponse(IRWSResponse odmResponse)
        {
            var odm = odmResponse as RWSResponse;

            OdmXmlDoc = XDocument.Parse(odm != null ? odm.RawXMLString() : "");
        }

        /// <summary>
        /// Starts this parser instance.
        /// </summary>
        public abstract void Start();

        /// <summary>
        /// Value prefix with ODM Namespace
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        protected static XName Odm(string value)
        {

            return XNamespace.Get(Constants.ODM_NS) + value;

        }


        /// <summary>
        /// Value prefix with mdsol Namespace
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        protected static XName Mdsol(string value)
        {
            return XNamespace.Get(Constants.MDSOL_NS) + value;
        }
    }
}
