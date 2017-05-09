using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medidata.RWS.Schema;

namespace Medidata.RWS.Core.DataBuilders
{
    /// <summary>
    /// A builder for constructing "StudyEventData" objects in a state suitable for transmission.
    /// </summary>
    /// <tocexclude />
    public class StudyEventDataBuilder : AuditableBuilderBase<ODMcomplexTypeDefinitionStudyEventData, StudyEventDataBuilder>, 
        SpecifiesTransactionType<StudyEventDataBuilder>
    {


        /// <summary>
        /// Initializes a new instance of the StudyEventDataBuilder class using the specified StudyEventOID value.
        /// </summary>
        /// <param name="studyEventOID"></param>
        /// <param name="studyEventRepeatKey"></param>
        public StudyEventDataBuilder(string studyEventOID)
        {

            thisNode = new ODMcomplexTypeDefinitionStudyEventData
            {
                StudyEventOID = studyEventOID,
                StudyEventRepeatKey = null,
                FormData = new List<ODMcomplexTypeDefinitionFormData>(),
                AuditRecord = null,
                Signature = null,
                TransactionType = TransactionType.Update //default transaction type
            };

        }


        /// <summary>
        /// Initializes a new instance of the StudyEventDataBuilder class using the specified StudyEventOID and StudyEventRepeatKey values.
        /// </summary>
        /// <param name="studyEventOID"></param>
        /// <param name="studyEventRepeatKey"></param>
        public StudyEventDataBuilder(string studyEventOID, int studyEventRepeatKey) : this(studyEventOID)
        {

            thisNode.StudyEventRepeatKey = studyEventRepeatKey.ToString();

        }

        /// <summary>
        /// Initializes a new instance of the StudyEventDataBuilder class using the specified StudyEventOID and StudyEventRepeatKey values.
        /// </summary>
        /// <param name="studyEventOID"></param>
        /// <param name="studyEventRepeatKey"></param>
        public StudyEventDataBuilder(string studyEventOID, string studyEventRepeatKey) : this(studyEventOID)
        {

            thisNode.StudyEventRepeatKey = studyEventRepeatKey;

        }


        /// <summary>
        /// Add a "FormData" node and set the "FormOID" and "FormRepeatKey" attributes.
        /// </summary>
        /// <param name="FormOID"></param>
        /// <param name="FormRepeatKey"></param>
        /// <param name="formDataBuilder"></param>
        /// <returns></returns>
        public StudyEventDataBuilder AddFormData(string FormOID, int FormRepeatKey, Action<FormDataBuilder> formDataBuilder)
        {

            var fdb = new FormDataBuilder(FormOID, FormRepeatKey);

            formDataBuilder(fdb);

            thisNode.FormData.Add(fdb.Build());

            return this;

        }

        /// <summary>
        /// Add a "FormData" node and set the "FormOID" attribute.
        /// </summary>
        /// <param name="FormOID"></param>
        /// <param name="FormRepeatKey"></param>
        /// <param name="formDataBuilder"></param>
        /// <returns></returns>
        public StudyEventDataBuilder AddFormData(string FormOID, Action<FormDataBuilder> formDataBuilder)
        {

            var fdb = new FormDataBuilder(FormOID);

            formDataBuilder(fdb);

            thisNode.FormData.Add(fdb.Build());

            return this;

        }

        /// <summary>
        /// Add a "FormData" node and set the "FormOID" and "FormRepeatKey" attributes.
        /// </summary>
        /// <param name="FormOID"></param>
        /// <param name="FormRepeatKey"></param>
        /// <param name="formDataBuilder"></param>
        /// <returns></returns>
        public StudyEventDataBuilder AddFormData(string FormOID, string FormRepeatKey, Action<FormDataBuilder> formDataBuilder)
        {

            var fdb = new FormDataBuilder(FormOID, FormRepeatKey);

            formDataBuilder(fdb);

            thisNode.FormData.Add(fdb.Build());

            return this;

        }

        /// <summary>
        /// Set the transaction type on the "StudyEventData" node.
        /// </summary>
        /// <param name="tranxType"></param>
        /// <returns></returns>
        public StudyEventDataBuilder WithTransactionType(TransactionType tranxType)
        {
            thisNode.TransactionType = tranxType;
            return this;
        }

        /// <summary>
        /// return current instance.
        /// </summary>
        /// <returns></returns>
        protected override StudyEventDataBuilder ThisAsTSelf()
        {
            return this;
        }
    }
}
