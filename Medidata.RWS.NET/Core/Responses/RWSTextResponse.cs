namespace Medidata.RWS.Core.Responses
{
    /// <summary>
    /// Represents a response from RWS in text format, which doesn't need to be parsed.
    /// </summary>
    public class RWSTextResponse : IRWSResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RWSTextResponse"/> class.
        /// </summary>
        /// <param name="responseText">The response text.</param>
        public RWSTextResponse(string responseText)
        {
            ResponseText = responseText;
        }

        /// <summary>
        /// Gets the response text.
        /// </summary>
        /// <value>
        /// The response text.
        /// </value>
        public string ResponseText { get; }
    }
}
