using Medidata.RWS.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Medidata.RWS.Core.Responses;

namespace Medidata.RWS.Core.RWSObjects
{
    /// <summary>
    /// Represents a list of Studies
    /// </summary>
    /// <seealso cref="RWSStudyListItem" />
    /// <seealso cref="Medidata.RWS.Core.Responses.IRWSResponse" />
    public class RWSStudies : IEnumerable<RWSStudyListItem>, IRWSResponse
    {
        List<RWSStudyListItem> StudyListItems = new List<RWSStudyListItem>();

        /// <summary>
        /// Initializes a new instance of the <see cref="RWSStudies"/> class.
        /// </summary>
        /// <param name="xmlString">The XML string.</param>
        public RWSStudies(string xmlString)
        {

            ODM odm = RWSHelpers.Serializers.XmlDeserializeFromString<ODM>(xmlString);

            foreach (var study in odm.Study)
            {
                StudyListItems.Add(new RWSStudyListItem(study));
            }

        }


        /// <summary>
        /// Gets or sets the <see cref="RWSStudyListItem"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="RWSStudyListItem"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public RWSStudyListItem this[int index]
        {
            get { return StudyListItems[index]; }
            set { StudyListItems.Insert(index, value); }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<RWSStudyListItem> GetEnumerator()
        {
            return this.StudyListItems.GetEnumerator();
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
    }
}
