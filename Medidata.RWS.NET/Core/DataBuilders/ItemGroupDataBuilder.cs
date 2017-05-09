using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Medidata.RWS.Schema;

namespace Medidata.RWS.Core.DataBuilders
{
    /// <summary>
    /// A builder for constructing "ItemGroupData" objects in a state suitable for transmission.
    /// </summary>
    /// <tocexclude />
    public class ItemGroupDataBuilder : Builds<ODMcomplexTypeDefinitionItemGroupData>, SpecifiesTransactionType<ItemGroupDataBuilder>
    {

        /// <summary>
        /// The object to be built.
        /// </summary>
        private ODMcomplexTypeDefinitionItemGroupData itemGroupdata;

        /// <summary>
        /// Initializes a new instance of the ItemGroupDataBuilder class using the specified ItemGroupOID value.
        /// </summary>
        /// <param name="itemGroupOid"></param>
        public ItemGroupDataBuilder(string itemGroupOid)
        {
            itemGroupdata = new ODMcomplexTypeDefinitionItemGroupData
            {
                ItemGroupOID = itemGroupOid,
                AuditRecord = null,
                ItemGroupRepeatKey = null,
                Signature = null,
                Items = new List<object>(),
                TransactionType = TransactionType.Update //default transaction type
            };
        }

        /// <summary>
        /// Initializes a new instance of the ItemGroupDataBuilder class using the specified ItemGroupOID
        /// and ItemGroupRepeatKey values.
        /// </summary>
        /// <param name="itemGroupOid"></param>
        /// <param name="itemGroupRepeatKey"></param>
        public ItemGroupDataBuilder(string itemGroupOid, string itemGroupRepeatKey) : this(itemGroupOid)
        {
            itemGroupdata.ItemGroupRepeatKey = itemGroupRepeatKey;
        }

        /// <summary>
        /// Add an "ItemData" node using the specified ItemOID and Value parameter values. 
        /// Returns the current builder instance.
        /// </summary>
        /// <param name="ItemOID"></param>
        /// <param name="Value"></param>
        /// <param name="itemDataBuilder"></param>
        /// <returns></returns>
        public ItemGroupDataBuilder AddItemData(string ItemOID, string Value, Action<ItemDataBuilder> itemDataBuilder)
        {

            var idb = new ItemDataBuilder(ItemOID, Value);

            itemDataBuilder(idb);

            itemGroupdata.Items.Add(idb.Build());

            return this;

        }


        /// <summary>
        /// See <see cref="Builds{T}.Build()"></see> for more information.
        /// </summary>
        /// <returns></returns>
        public ODMcomplexTypeDefinitionItemGroupData Build()
        {
            return itemGroupdata;
        }


        /// <summary>
        /// Set the transaction type on the "ItemGroupData" node. Returns the current builder instance.
        /// </summary>
        /// <param name="tranxType"></param>
        /// <returns></returns>
        public ItemGroupDataBuilder WithTransactionType(TransactionType tranxType)
        {

            itemGroupdata.TransactionType = tranxType;
            return this;

        }


        /// <summary>
        /// Remove all empty (blank value) ItemData nodes that currently exist.
        /// </summary>
        public void RemoveEmptyNodes()
        {
            foreach(var item in itemGroupdata.Items.ToList())
            {
                dynamic d = item;
                if (string.IsNullOrEmpty(d.Value))
                {
                    itemGroupdata.Items.Remove(item);
                }
            }
        }
    }
}
