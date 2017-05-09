using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medidata.RWS.Schema;

namespace Medidata.RWS.Core.DataBuilders
{

    /// <summary>
    /// A builder for constructing "FormData" objects in a state suitable for transmission.
    /// </summary>
    /// <tocexclude />
    public class FormDataBuilder : Builds<ODMcomplexTypeDefinitionFormData>, SpecifiesTransactionType<FormDataBuilder>
    {

        /// <summary>
        /// The FormData object to be built.
        /// </summary>
        private ODMcomplexTypeDefinitionFormData formData;

        /// <summary>
        /// Initializes a new instance of the FormDataBuilder class using the specified FormOID and FormRepeatKey values.
        /// </summary>
        /// <param name="formOID"></param>
        /// <param name="formRepeatKey"></param>
        /// 
        public FormDataBuilder(string formOID, int formRepeatKey) : this(formOID)
        {
            formData.FormRepeatKey = formRepeatKey.ToString();
        }


        /// <summary>
        /// Initializes a new instance of the FormDataBuilder class using the specified FormOID and FormRepeatKey values.
        /// </summary>
        /// <param name="formOID"></param>
        /// <param name="formRepeatKey"></param>
        /// 
        public FormDataBuilder(string formOID, string formRepeatKey) : this(formOID)
        {
            formData.FormRepeatKey = formRepeatKey;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormDataBuilder"/> class using a FormOID only.
        /// </summary>
        /// <param name="formOID">The form OID.</param>
        public FormDataBuilder(string formOID)
        {
            formData = new ODMcomplexTypeDefinitionFormData
            {
                FormOID = formOID,
                FormRepeatKey = null,
                AuditRecord = null,
                Signature = null,
                ArchiveLayoutRef = null,
                TransactionType = TransactionType.Update //default transaction type
            };
        }


        /// <summary>
        /// See <see cref="Builds{T}.Build()"></see> for more information.
        /// </summary>
        /// <returns></returns>
        public ODMcomplexTypeDefinitionFormData Build()
        {
            return formData;
        }

        /// <summary>
        /// Add an "ItemGroupData" node using the specified "ItemGroupOID" value. 
        /// Returns the current builder instance.
        /// </summary>
        /// <param name="ItemGroupOID"></param>
        /// <param name="itemGroupDataBuilder"></param>
        /// <returns></returns>
        public FormDataBuilder AddItemGroupData(string ItemGroupOID, Action<ItemGroupDataBuilder> itemGroupDataBuilder)
        {

            var igdb = new ItemGroupDataBuilder(ItemGroupOID);

            itemGroupDataBuilder(igdb);

            formData.ItemGroupData.Add(igdb.Build());

            return this;

        }

        /// <summary>
        /// Add an "ItemGroupData" node using the specified "ItemGroupOID" value. 
        /// Returns the current builder instance.
        /// </summary>
        /// <param name="ItemGroupOID"></param>
        /// <param name="ItemGroupRepeatKey"></param>
        /// <param name="itemGroupDataBuilder"></param>
        /// <returns></returns>
        public FormDataBuilder AddItemGroupData(string ItemGroupOID, string ItemGroupRepeatKey, Action<ItemGroupDataBuilder> itemGroupDataBuilder)
        {

            var igdb = new ItemGroupDataBuilder(ItemGroupOID, ItemGroupRepeatKey);

            itemGroupDataBuilder(igdb);

            formData.ItemGroupData.Add(igdb.Build());

            return this;

        }

        /// <summary>
        /// Set the transaction type on the "FormData" node. Returns the current builder instance.
        /// If the transaction type is "Upsert", the repeat key is set to "@CONTEXT".
        /// </summary>
        /// <param name="tranxType"></param>
        /// <returns></returns>
        public FormDataBuilder WithTransactionType(TransactionType tranxType)
        {
            formData.TransactionType = tranxType;
            if (tranxType == TransactionType.Upsert)
            {
                formData.FormRepeatKey = "@CONTEXT";
            }
            return this;
        }

    }
}
