using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medidata.RWS.Extras.AuditEvent
{

    /// <summary>
    /// Subject related attributes
    /// </summary>
    /// <seealso cref="Medidata.RWS.Extras.AuditEvent.ContextBase" />
    public class Subject : ContextBase
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="Subject"/> class.
        /// </summary>
        /// <param name="Key">The key.</param>
        /// <param name="Name">The name.</param>
        /// <param name="Status">The status.</param>
        /// <param name="TransactionType">Type of the transaction.</param>
        public Subject(string Key, string Name, string Status, string TransactionType, string SubjectKeyType)
        {
            this.Key = Key;
            this.Name = Name;
            this.Status = Status;
            this.TransactionType = TransactionType;
            this.SubjectKeyType = SubjectKeyType;
        }

        /// <summary>
        /// Gets the type of the subject key.
        /// </summary>
        /// <value>
        /// The type of the subject key.
        /// </value>
        public string SubjectKeyType { get; private set; }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key { get; private set; }
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; private set; }
        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; private set; }
        /// <summary>
        /// Gets the type of the transaction.
        /// </summary>
        /// <value>
        /// The type of the transaction.
        /// </value>
        public string TransactionType { get; private set; }
    }
}
