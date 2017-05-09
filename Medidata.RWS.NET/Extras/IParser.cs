using Medidata.RWS.Core.Responses;

namespace Medidata.RWS.Extras
{
    /// <summary>
    /// Parser interface for ODM data
    /// </summary>
    public interface IParser
    {

        /// <summary>
        /// Sets the RWS response.
        /// </summary>
        /// <param name="odmResponse">The odm response.</param>
        void SetResponse(IRWSResponse odmResponse);

        /// <summary>
        /// Starts this parser instance.
        /// </summary>
        void Start();

        /// <summary>
        /// Fluent method that assigns the RWS response.
        /// </summary>
        /// <param name="odmResponse">The odm response.</param>
        /// <returns></returns>
        IParser ForOdmResponse(IRWSResponse odmResponse);

    }
}