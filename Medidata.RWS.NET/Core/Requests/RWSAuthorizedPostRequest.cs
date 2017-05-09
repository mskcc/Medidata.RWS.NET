namespace Medidata.RWS.Core.Requests
{
    /// <summary>
    /// Represents a POST requires that requires authentication.
    /// </summary>
    /// <seealso cref="Medidata.RWS.Core.Requests.RWSPostRequest" />
    public abstract class RWSAuthorizedPostRequest : RWSPostRequest
    {
        /// <summary>
        /// Whether or not the request requires authentication
        /// </summary>
        public override bool RequiresAuthentication { get { return true; } }

    }
}