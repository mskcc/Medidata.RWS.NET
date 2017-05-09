namespace Medidata.RWS.Core.Responses
{ 
    using RestSharp;

    /// <summary>
    /// Represents a response based on ClinicalData post messages, which have additional attributes to that
    /// of a "normal" RWS Response message.
    /// </summary>
    public class RWSPostErrorResponse : RWSResponse
    {
        /// <summary>
        /// The error client response message
        /// </summary>
        public readonly string ErrorClientResponseMessage;
        /// <summary>
        /// The error origin location
        /// </summary>
        public readonly string ErrorOriginLocation;
        /// <summary>
        /// The reason code
        /// </summary>
        public readonly string ReasonCode;


        /// <summary>
        /// Initializes a new instance of the <see cref="RWSPostErrorResponse"/> class.
        /// </summary>
        /// <param name="response">The response.</param>
        public RWSPostErrorResponse(IRestResponse response) : base(response)
        {

            this.ReasonCode = rootNode.GetAttribute("ReasonCode");
            this.ErrorOriginLocation = rootNode.GetAttribute("ErrorOriginLocation");
            this.ErrorClientResponseMessage = rootNode.GetAttribute("ErrorClientResponseMessage");

        }
    }
}
