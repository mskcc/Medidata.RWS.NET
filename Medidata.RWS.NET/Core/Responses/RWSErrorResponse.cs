using Medidata.RWS.Core.Responses;
using System.Xml;

namespace Medidata.RWS.Core.Responses
{

    /// <summary>
    /// Parses XML that represents a RWS Response with an error message.
    /// </summary>
    /// <seealso cref="Medidata.RWS.Core.Responses.RWSXMLResponse" />
    public class RWSErrorResponse : RWSXMLResponse
    {

        /// <summary>
        /// The reference number
        /// </summary>
        public readonly string ReferenceNumber;

        /// <summary>
        /// The inbound odm file oid
        /// </summary>
        public readonly string InboundODMFileOID;

        /// <summary>
        /// The reason code
        /// </summary>
        public readonly string ReasonCode;

        /// <summary>
        /// Whether or not the transaction was successful.
        /// </summary>
        public readonly bool IsTransactionSuccessful;

        /// <summary>
        /// The error description
        /// </summary>
        public readonly string ErrorDescription;


        /// <summary>
        /// Initializes a new instance of the <see cref="RWSErrorResponse"/> class.
        /// </summary>
        /// <param name="xmlString"></param>
        public RWSErrorResponse(string xmlString) : base(xmlString)
        {

            rootNode = base.ParseXMLString(xmlString);

            ReferenceNumber = rootNode.GetAttribute("ReferenceNumber");
            InboundODMFileOID = rootNode.GetAttribute("InboundODMFileOID");
            IsTransactionSuccessful = rootNode.GetAttribute("IsTransactionSuccessful") == "1";
            ReasonCode = rootNode.GetAttribute("ReasonCode");
            ErrorDescription = rootNode.GetAttribute("ErrorClientResponseMessage");

        }


    }
}