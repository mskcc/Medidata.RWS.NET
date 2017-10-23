using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using Medidata.RWS.Core.DataBuilders;
using Medidata.RWS.Schema;
using System.Xml.Linq;

namespace Medidata.RWS.Core.Responses
{
    /// <summary>
    /// Abstract class that represnts a response from RWS in XML format.
    /// </summary>
    public abstract class RWSXMLResponse : IRWSResponse
    {
        /// <summary>
        /// The raw XML, in string form, of the response.
        /// </summary>
        /// <returns></returns>
        public virtual string RawXMLString()
        {

            return xmlString;

        }

        /// <summary>
        /// Gets or sets the x document.
        /// </summary>
        /// <value>
        /// The x document.
        /// </value>
        public XDocument GetXDocument()
        {
            return RWSHelpers.Xml.GetXDocumentFromString(xmlString); 
        }

        /// <summary>
        /// Roots the XML node.
        /// </summary>
        /// <returns></returns>
        public XmlElement RootXMLNode()
        {
            return rootNode;
        }

        /// <summary>
        /// The root node
        /// </summary>
        protected XmlElement rootNode;

        private string xmlString;

        /// <summary>
        /// Create a new instance with an XML string.
        /// </summary>
        /// <param name="xmlString"></param>
        protected RWSXMLResponse(string xmlString)
        {
            xmlString = string.IsNullOrEmpty(xmlString) ? string.Empty : xmlString;
            this.xmlString = xmlString;
            rootNode = ParseXMLString(xmlString);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RWSXMLResponse"/> class.
        /// </summary>
        protected RWSXMLResponse()
        {

        }

        /// <summary>
        /// Parse a string representation of a RWS XML Response.
        /// </summary>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public XmlElement ParseXMLString(string xmlString)
        {
            return RWSHelpers.Xml.GetXmlElementFromString(xmlString);
        }


        /// <summary>
        /// Gets the first element with a specific attribute value.
        /// </summary>
        /// <param name="elementName">Name of the element.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="attributeValue">The attribute value.</param>
        /// <returns></returns>
        public XElement GetFirstElementWithAttributeValue(string elementName, string attributeName,
            string attributeValue)
        {

            return XElement.Parse(RWSHelpers.Xml.RemoveInvalidXmlChars(RawXMLString()))
                .Descendants(XNamespace.Get(Constants.ODM_NS) + elementName)
                .FirstOrDefault(el =>
                {
                    var xAttribute = el.Attribute(attributeName);
                    return xAttribute != null && (attributeValue != null && xAttribute.Value == attributeValue);
                });

        }


        /// <summary>
        /// Gets all elements with a specific attribute value.
        /// </summary>
        /// <param name="elementName">Name of the element.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="attributeValue">The attribute value.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerable<XElement> GetAllElementsWithAttributeValue(string elementName, string attributeName,
            string attributeValue)
        {
            return XElement.Parse(RWSHelpers.Xml.RemoveInvalidXmlChars(RawXMLString()))
                .Descendants(XNamespace.Get(Constants.ODM_NS) + elementName)
                .Where(el =>
                {
                    var xAttribute = el.Attribute(attributeName);
                    return xAttribute != null && (attributeValue != null && xAttribute.Value == attributeValue);
                });
        }

    }

}
