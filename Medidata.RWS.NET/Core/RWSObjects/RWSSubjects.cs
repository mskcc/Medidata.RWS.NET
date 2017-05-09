using Medidata.RWS.Core.Responses;
using Medidata.RWS.Schema;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medidata.RWS.Core.RWSObjects
{
    /// <summary>
    /// Represents a list of subjects.
    /// </summary>
    /// <seealso cref="RWSSubjectListItem" />
    /// <seealso cref="Medidata.RWS.Core.Responses.IRWSResponse" />
    public class RWSSubjects : IEnumerable<RWSSubjectListItem>, IRWSResponse
    {

        List<RWSSubjectListItem> SubjectListItems = new List<RWSSubjectListItem>();

        /// <summary>
        /// Gets or sets the <see cref="RWSSubjectListItem"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="RWSSubjectListItem"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public RWSSubjectListItem this[int index]
        {
            get { return SubjectListItems[index]; }
            set { SubjectListItems.Insert(index, value); }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<RWSSubjectListItem> GetEnumerator()
        {
            return SubjectListItems.GetEnumerator();
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
        /// Initializes a new instance of the <see cref="RWSSubjects"/> class.
        /// </summary>
        /// <param name="xmlString">The XML string.</param>
        public RWSSubjects(string xmlString)
        {

            ODM odm = RWSHelpers.Serializers.XmlDeserializeFromString<ODM>(xmlString);

            foreach (var clinData in odm.ClinicalData)
            {
                SubjectListItems.Add(new RWSSubjectListItem(clinData));
            }

        }



    }
}
