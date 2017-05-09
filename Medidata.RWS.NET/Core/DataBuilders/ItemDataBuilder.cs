using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medidata.RWS.Schema;

namespace Medidata.RWS.Core.DataBuilders
{

    /// <summary>
    /// A builder for constructing "ItemData" objects in a state suitable for transmission.
    /// </summary>
    /// <tocexclude />
    public class ItemDataBuilder : Builds<object>, SpecifiesTransactionType<ItemDataBuilder>
    {
        /// <summary>
        /// The ItemData object to be built.
        /// </summary>
        private ODMcomplexTypeDefinitionItemData itemData;

        /// <summary>
        /// Initializes a new instance of the ItemDataBuilder class using the specified ItemOID and Value values.
        /// </summary>
        /// <param name="itemOID"></param>
        /// <param name="value"></param>
        public ItemDataBuilder(string itemOID, string value)
        {
            itemData = new ODMcomplexTypeDefinitionItemData
            {
                ItemOID = itemOID,
                Value = value,
                AuditRecord = null,
                Annotation = null,
                MeasurementUnitRef = null,
                Signature = null,
                TransactionType = TransactionType.Update //default transaction type
            };
        }

        /// <summary>
        /// See <see cref="Builds{T}.Build()"></see> for more information.
        /// </summary>
        /// <returns></returns>
        public object Build()
        {
            return itemData;
        }

        /// <summary>
        /// Add an AuditRecord node to this object using the specified parameter values.  Returns the current builder instance.
        /// </summary>
        /// <param name="UserOID"></param>
        /// <param name="LocationOID"></param>
        /// <param name="ReasonForChange"></param>
        /// <param name="SourceID"></param>
        /// <param name="DateTimeStamp"></param>
        /// <param name="auditRecordBuilder"></param>
        /// <returns></returns>
        public ItemDataBuilder WithAuditRecord(string UserOID, string LocationOID, string ReasonForChange, string SourceID, DateTime DateTimeStamp, Action<AuditRecordBuilder> auditRecordBuilder)
        {

            var arb = new AuditRecordBuilder(UserOID, LocationOID, ReasonForChange, SourceID, DateTimeStamp);

            auditRecordBuilder(arb);

            itemData.AuditRecord = arb.Build();

            return this;

        }

        /// <summary>
        /// Adds an audit record to this instance.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <returns>
        /// The ItemDataBuilder.
        /// </returns>
        public ItemDataBuilder WithAuditRecord(ODMcomplexTypeDefinitionAuditRecord record)
        {
            itemData.AuditRecord = record;
            return this;
        }

        /// <summary>
        /// Set the transaction type on the "ItemData" node.
        /// </summary>
        /// <param name="tranxType"></param>
        /// <returns></returns>
        public ItemDataBuilder WithTransactionType(TransactionType tranxType)
        {

            itemData.TransactionType = tranxType;
            return this;

        }
    }
}
