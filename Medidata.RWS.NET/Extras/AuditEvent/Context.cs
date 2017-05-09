using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medidata.RWS.Extras.AuditEvent
{


    /// <summary>
    /// Custom event args class that holds a reference to a context object.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class ContextEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        public Context Context { get; set; }
    }


    /// <summary>
    /// Custom event args class that holds a reference to an ODM Adapter instance.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class ODMAdapterEventArgs : EventArgs
    {

        /// <summary>
        /// Gets or sets the last source identifier.
        /// </summary>
        /// <value>
        /// The last source identifier.
        /// </value>
        public int LastSourceID { get; set; }
    }

    





    /// <summary>
    /// Context interface.
    /// </summary>
    public interface IContextElement { }

    /// <summary>
    /// Interface for nodes that have user related elements.
    /// </summary>
    public interface HasUserElements
    {
        /// <summary>
        /// Gets or sets the date time stamp.
        /// </summary>
        /// <value>
        /// The date time stamp.
        /// </value>
        DateTime DateTimeStamp { get; set; }
        /// <summary>
        /// Gets or sets the location oid.
        /// </summary>
        /// <value>
        /// The location oid.
        /// </value>
        string LocationOID { get; set; }
        /// <summary>
        /// Gets or sets the user oid.
        /// </summary>
        /// <value>
        /// The user oid.
        /// </value>
        string UserOID { get; set; }
    }

    /// <summary>
    /// Base class for context objects.
    /// </summary>
    /// <seealso cref="Medidata.RWS.Extras.AuditEvent.IContextElement" />
    public class ContextBase : IContextElement
    {
    }


    /// <summary>
    /// A context class that holds the data for every reportable element in the ODM response
    /// </summary>
    /// <seealso cref="Medidata.RWS.Extras.AuditEvent.ContextBase" />
    public class Context : ContextBase
    {
        /// <summary>
        /// Gets the metadata version.
        /// </summary>
        /// <value>
        /// The metadata version.
        /// </value>
        public int MetadataVersion { get; private set; }
        /// <summary>
        /// Gets the study oid.
        /// </summary>
        /// <value>
        /// The study oid.
        /// </value>
        public string StudyOID { get; private set; }
        /// <summary>
        /// Gets the sub category.
        /// </summary>
        /// <value>
        /// The sub category.
        /// </value>
        public string SubCategory { get; private set; }
        /// <summary>
        /// Gets or sets the study event.
        /// </summary>
        /// <value>
        /// The study event.
        /// </value>
        public StudyEvent StudyEvent { get; set; }
        /// <summary>
        /// Gets or sets the item group.
        /// </summary>
        /// <value>
        /// The item group.
        /// </value>
        public ItemGroup ItemGroup { get;  set; }
        /// <summary>
        /// Gets or sets the form.
        /// </summary>
        /// <value>
        /// The form.
        /// </value>
        public Form Form { get; set; }
        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>
        /// The item.
        /// </value>
        public Item  Item { get; set; }
        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public Subject Subject { get; set; }
        /// <summary>
        /// Gets or sets the audit record.
        /// </summary>
        /// <value>
        /// The audit record.
        /// </value>
        public AuditRecord AuditRecord { get; set; }
        /// <summary>
        /// Gets or sets the query.
        /// </summary>
        /// <value>
        /// The query.
        /// </value>
        public Query Query { get; set; }
        /// <summary>
        /// Gets or sets the protocol deviation.
        /// </summary>
        /// <value>
        /// The protocol deviation.
        /// </value>
        public ProtocolDeviation ProtocolDeviation { get; set; }
        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public Comment Comment { get; set; }
        /// <summary>
        /// Gets or sets the review.
        /// </summary>
        /// <value>
        /// The review.
        /// </value>
        public Review Review { get; set; }
        /// <summary>
        /// Gets or sets the signature.
        /// </summary>
        /// <value>
        /// The signature.
        /// </value>
        public Signature Signature { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Context"/> class.
        /// </summary>
        /// <param name="studyoid">The studyoid.</param>
        /// <param name="subcategory">The subcategory.</param>
        /// <param name="metadata_version">The metadata version.</param>
        public Context(string studyoid, string subcategory, int metadata_version)
        {
            StudyOID = studyoid;
            SubCategory = subcategory;
            MetadataVersion = metadata_version;
            AuditRecord = new AuditRecord();
            Signature = new Signature();
        }

    }

}
