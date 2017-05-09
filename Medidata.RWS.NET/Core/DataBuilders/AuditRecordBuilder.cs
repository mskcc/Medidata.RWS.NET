using Medidata.RWS;
using Medidata.RWS.Core.DataBuilders;
using Medidata.RWS.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medidata.RWS.Core.DataBuilders
{

    /// <summary>
    /// A builder for constructing "AuditRecord" objects in a state suitable for transmission.
    /// </summary>
    /// <tocexclude />
    public class AuditRecordBuilder : Builds<ODMcomplexTypeDefinitionAuditRecord>
    {

        /// <summary>
        /// The AuditRecord object to be built.
        /// </summary>
        private ODMcomplexTypeDefinitionAuditRecord auditRecord;


        /// <summary>
        /// Initializes a new instance of the AuditRecordBuilder class using the specified parameter values.
        /// </summary>
        /// <param name="userOID"></param>
        /// <param name="locationOID"></param>
        /// <param name="reasonForChange"></param>
        /// <param name="sourceID"></param>
        /// <param name="dateTimeStamp"></param>
        public AuditRecordBuilder(string userOID, string locationOID, string reasonForChange, string sourceID, DateTime dateTimeStamp)
        {

            auditRecord = new ODMcomplexTypeDefinitionAuditRecord
            {
                UserRef = new ODMcomplexTypeDefinitionUserRef
                {
                    UserOID = userOID
                },
                DateTimeStamp = new ODMcomplexTypeDefinitionDateTimeStamp
                {
                    Value = dateTimeStamp
                }, 
                EditPoint = EditPointType.DataManagement,
                LocationRef = new ODMcomplexTypeDefinitionLocationRef {  LocationOID = locationOID },
                ReasonForChange = new ODMcomplexTypeDefinitionReasonForChange { Value = reasonForChange },
                SourceID = new ODMcomplexTypeDefinitionSourceID {  Value = sourceID },
                UsedImputationMethod = YesOrNo.No,
                ID = null
            };

        }

        /// <summary>
        /// See <see cref="Builds{T}.Build()"></see> for more information.
        /// </summary>
        /// <returns></returns>
        public ODMcomplexTypeDefinitionAuditRecord Build()
        {
            return auditRecord;
        }
    }
}
