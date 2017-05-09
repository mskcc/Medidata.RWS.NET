using Medidata.RWS.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Medidata.RWS.Schema;

namespace Medidata.RWS.Core.RWSObjects
{
    /// <summary>
    /// Represents a list of study versions.
    /// </summary>
    /// <seealso cref="RWSMetaDataVersion" />
    /// <seealso cref="Medidata.RWS.Core.Responses.IRWSResponse" />
    public class RWSStudyMetadataVersions : IEnumerable<RWSMetaDataVersion>, IRWSResponse
    {

        /// <summary>
        /// The study
        /// </summary>
        public readonly RWSStudyListItem Study;

        /// <summary>
        /// The meta data versions
        /// </summary>
        public readonly List<RWSMetaDataVersion> MetaDataVersions = new List<RWSMetaDataVersion>();

        /// <summary>
        /// Gets or sets the <see cref="RWSMetaDataVersion"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="RWSMetaDataVersion"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public RWSMetaDataVersion this[int index]
        {
            get { return MetaDataVersions[index]; }
            set { MetaDataVersions.Insert(index, value); }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<RWSMetaDataVersion> GetEnumerator()
        {
            return MetaDataVersions.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RWSStudyMetadataVersions"/> class.
        /// </summary>
        /// <param name="xmlString">The XML string.</param>
        public RWSStudyMetadataVersions(string xmlString)
        {

            ODM odm = RWSHelpers.Serializers.XmlDeserializeFromString<ODM>(xmlString);

            if (odm.Study.Count == 0) return;

            Study = new RWSStudyListItem(odm.Study.First());

            foreach (var version in odm.Study.First().MetaDataVersion)
            {
                MetaDataVersions.Add(new RWSMetaDataVersion(version));
            }

        }


    }
}
