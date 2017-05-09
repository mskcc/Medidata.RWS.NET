namespace Medidata.RWS.Core.RWSObjects
{
    using Medidata.RWS.Schema;

    /// <summary>
    /// Represents a study CRF Version.
    /// </summary>
    public class RWSMetaDataVersion
    {
        /// <summary>
        /// The Study OID.
        /// </summary>
        public readonly string OID;
        /// <summary>
        /// The Study Name.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Initializes a new instance of the <see cref="RWSMetaDataVersion"/> class.
        /// </summary>
        /// <param name="version">The version.</param>
        public RWSMetaDataVersion(ODMcomplexTypeDefinitionMetaDataVersion version)
        {
            OID = version.OID;
            Name = version.Name;
        }
    }
}
