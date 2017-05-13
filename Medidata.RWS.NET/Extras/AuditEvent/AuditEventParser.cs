using Medidata.RWS.Core.Responses;
using System;
using System.Linq;
using System.Xml.Linq;

namespace Medidata.RWS.Extras.AuditEvent
{

    /// <summary>
    /// Parse ODM data, which in turn builds out a context object that represents a single audit record.
    /// </summary>
    public class AuditEventParser : AbstractParser, IAuditEventParser
    {

        /// <summary>
        /// Occurs when the context object has been built.
        /// </summary>
        public event ContextBuiltEventHandler ContextBuilt;


        // ===========
        // Elements
        // ===========
        private readonly XName E_CLINICAL_DATA = Odm("ClinicalData");
        private readonly XName E_SUBJECT_DATA = Odm("SubjectData");
        private readonly XName E_STUDYEVENT_DATA = Odm("StudyEventData");
        private readonly XName E_USER_REF = Odm("UserRef");
        private readonly XName E_SOURCE_ID = Odm("SourceID");
        private readonly XName E_DATE_TIME_STAMP = Odm("DateTimeStamp");
        private readonly XName E_REASON_FOR_CHANGE = Odm("ReasonForChange");
        private readonly XName E_LOCATION_REF = Odm("LocationRef");
        private readonly XName E_FORM_DATA = Odm("FormData");
        private readonly XName E_ITEM_GROUP_DATA = Odm("ItemGroupData");
        private readonly XName E_ITEM_DATA = Odm("ItemData");
        private readonly XName E_QUERY = Mdsol("Query");
        private readonly XName E_PROTOCOL_DEVIATION = Mdsol("ProtocolDeviation");
        private readonly XName E_REVIEW = Mdsol("Review");
        private readonly XName E_COMMENT = Mdsol("Comment");
        private readonly XName E_SIGNATURE = Odm("Signature");
        private readonly XName E_SIGNATURE_REF = Odm("SignatureRef");

        // ===========
        // Attributes
        // ===========
        private readonly XName A_AUDIT_SUBCATEGORY_NAME = Mdsol("AuditSubCategoryName");
        private readonly XName A_mdsol_SUBJECT_KEY_TYPE = Mdsol("SubjectKeyType");
        private readonly XName A_METADATA_VERSION_OID = "MetaDataVersionOID";
        private readonly XName A_STUDY_OID = "StudyOID";
        private readonly XName A_TRANSACTION_TYPE = "TransactionType";
        private readonly XName A_SUBJECT_NAME = Mdsol("SubjectName");
        private readonly XName A_SUBJECT_KEY = "SubjectKey";
        private readonly XName A_USER_OID = "UserOID";
        private readonly XName A_LOCATION_OID = "LocationOID";
        private readonly XName A_ITEM_OID = "ItemOID";
        private readonly XName A_VALUE = "Value";
        private readonly XName A_STUDYEVENT_OID = "StudyEventOID";
        private readonly XName A_STUDYEVENT_REPEAT_KEY = "StudyEventRepeatKey";
        private readonly XName A_RECORD_ID = Mdsol("RecordId");
        private readonly XName A_FORM_OID = "FormOID";
        private readonly XName A_FORM_REPEAT_KEY = "FormRepeatKey";
        private readonly XName A_ITEMGROUP_OID = "ItemGroupOID";
        private readonly XName A_ITEMGROUP_REPEAT_KEY = "ItemGroupRepeatKey";
        private readonly XName A_QUERY_REPEAT_KEY = "QueryRepeatKey";
        private readonly XName A_STATUS = "Status";
        private readonly XName A_RECIPIENT = "Recipient";
        private readonly XName A_RESPONSE = "Response";
        private readonly XName A_FREEZE = Mdsol("Freeze");
        private readonly XName A_VERIFY = Mdsol("Verify");
        private readonly XName A_LOCK = Mdsol("Lock");
        private readonly XName A_SUBJECT_STATUS = Mdsol("Status");
        private readonly XName A_PROTCOL_DEVIATION_REPEAT_KEY = "ProtocolDeviationRepeatKey";
        private readonly XName A_CLASS = "Class";  // PV
        private readonly XName A_CODE = "Code";  // PV
        private readonly XName A_REVIEWED = "Reviewed";  // Reviews
        private readonly XName A_GROUP_NAME = "GroupName";
        private readonly XName A_COMMENT_REPEAT_KEY = "CommentRepeatKey";
        private readonly XName A_INSTANCE_ID = Mdsol("InstanceId");
        private readonly XName A_INSTANCE_NAME = Mdsol("InstanceName");
        private readonly XName A_INSTANCE_OVERDUE = Mdsol("InstanceOverdue");
        private readonly XName A_DATAPAGE_NAME = Mdsol("DataPageName");
        private readonly XName A_DATAPAGE_ID = Mdsol("DataPageId");
        private readonly XName A_SIGNATURE_OID = "SignatureOID";


        // Parser States
        private const int STATE_NONE = 0;
        private static int STATE_SOURCE_ID = 1;
        private static int STATE_DATETIME = 2;
        private static int STATE_REASON_FOR_CHANGE = 3;

        // Signature elements have some of the same elements as Audits. Tells us which we are collecting
        private const int AUDIT_REF_STATE = 0;
        private const int SIGNATURE_REF_STATE = 1;

        /// <summary>
        /// Gets the state of the reference.
        /// </summary>
        /// <value>
        /// The state of the reference.
        /// </value>
        public int REF_STATE { get; private set; }

        /// <summary>
        /// State controls when we are looking for element text content what attribute to put it in
        /// </summary>
        public int STATE { get; private set; }

        /// <summary>
        /// The context object being constructed.
        /// </summary>
        public Context Context;

        /// <summary>
        /// The number of records processed.
        /// </summary>
        public int Count = 0;


        /// <summary>
        /// Gets the last source identifier.
        /// </summary>
        /// <value>
        /// The last source identifier.
        /// </value>
        public int LastSourceID { get; private set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="AuditEventParser"/> class.
        /// </summary>
        /// <param name="odmResponse">The odm response.</param>
        public AuditEventParser(IRWSResponse odmResponse)
        {

            SetResponse(odmResponse);

            REF_STATE = AUDIT_REF_STATE;

            STATE = STATE_NONE;

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditEventParser"/> class.
        /// </summary>
        public AuditEventParser()
        {
        }


        /// <summary>
        /// Called when the context object has been built.
        /// </summary>
        /// <param name="context">The context.</param>
        protected virtual void OnContextBuilt(Context context)
        {
            Count += 1;

            ContextBuilt?.Invoke(this, new ContextEventArgs() { Context = context });

        }

        /// <summary>
        /// Begin parsing the records, building up a context object
        /// </summary>
        public override void Start()
        {
            //Loop through each clinical data element
            foreach (var cData in OdmXmlDoc.Descendants(E_CLINICAL_DATA))
            {
                //reset ref state
                REF_STATE = AUDIT_REF_STATE;

                //Establish context object
                Context = new Context(
                    cData.Attribute(A_STUDY_OID).Value,
                    cData.Attribute(A_AUDIT_SUBCATEGORY_NAME).Value,
                    TryInt(cData.Attribute(A_METADATA_VERSION_OID).Value));

                //Subject Data
                var subjectData = GetElementsFrom(cData, E_SUBJECT_DATA);    

                if(subjectData != null)
                {
                    Context.Subject = new Subject(
                        GetAttributeValueFrom(subjectData, A_SUBJECT_KEY),
                        GetAttributeValueFrom(subjectData, A_SUBJECT_NAME),
                        GetAttributeValueFrom(subjectData, A_SUBJECT_STATUS),
                        GetAttributeValueFrom(subjectData, A_TRANSACTION_TYPE),
                        GetAttributeValueFrom(subjectData, A_mdsol_SUBJECT_KEY_TYPE)
                    );
                }

                // Study Event Data
                var studyEventData = GetElementsFrom(cData, E_STUDYEVENT_DATA);

                if(studyEventData != null)
                {
                    Context.StudyEvent = new StudyEvent(
                        GetAttributeValueFrom(studyEventData, A_STUDYEVENT_OID),
                        GetAttributeValueFrom(studyEventData, A_STUDYEVENT_REPEAT_KEY),
                        GetAttributeValueFrom(studyEventData, A_TRANSACTION_TYPE),
                        GetAttributeValueFrom(studyEventData, A_INSTANCE_NAME),
                        GetAttributeValueFrom(studyEventData, A_INSTANCE_OVERDUE),
                        TryInt(GetAttributeValueFrom(studyEventData, A_INSTANCE_ID))
                   );
                }

                //User Ref data
                var userRefData = cData.Descendants().FirstOrDefault(x => x.Name == E_USER_REF);

                if(userRefData != null)
                {
                    var ctxt = AuditOrSignatureElement();
                    ctxt.UserOID = userRefData.Attribute(A_USER_OID) == null ? "" : userRefData.Attribute(A_USER_OID).Value;
                }

                //Source ID data
                var sourceId = TryInt(cData.Descendants().FirstOrDefault(x => x.Name == E_SOURCE_ID).Value);
                Context.AuditRecord.SourceID = sourceId;
                LastSourceID = sourceId;

                //DateTimeStamp data
                var dtStamp = cData.Descendants().FirstOrDefault(x => x.Name == E_DATE_TIME_STAMP);
                if (dtStamp != null)
                {
                    AuditOrSignatureElement().DateTimeStamp = DateTime.Parse(dtStamp.Value);
                }

                //Reason for Change data
                var reasonForChange = cData.Descendants().FirstOrDefault(x => x.Name == E_REASON_FOR_CHANGE);
                if (reasonForChange != null)
                {
                    Context.AuditRecord.ReasonForChange = reasonForChange.Value;
                }

                // Location Ref Data
                var locationRefData = cData.Descendants().FirstOrDefault(x => x.Name == E_LOCATION_REF);

                if (locationRefData != null)
                {
                    AuditOrSignatureElement().LocationOID = locationRefData.Attribute(A_LOCATION_OID) == null ? "" : locationRefData.Attribute(A_LOCATION_OID).Value;
                }

                //Form Data
                var formData = cData.Descendants().FirstOrDefault(x => x.Name == E_FORM_DATA);

                if (formData != null)
                {
                    Context.Form = new Form(
                       formData.Attribute(A_FORM_OID) == null ? "" : formData.Attribute(A_FORM_OID).Value,
                       formData.Attribute(A_FORM_REPEAT_KEY) == null ? 0 : TryInt(formData.Attribute(A_FORM_REPEAT_KEY).Value),
                       formData.Attribute(A_TRANSACTION_TYPE) == null ? "" : formData.Attribute(A_TRANSACTION_TYPE).Value,
                       formData.Attribute(A_DATAPAGE_NAME) == null ? "" : formData.Attribute(A_DATAPAGE_NAME).Value,
                       formData.Attribute(A_DATAPAGE_ID) == null ? -1 : TryInt(formData.Attribute(A_DATAPAGE_ID).Value)
                      
                   );
                }

                //ItemGroup Data
                var itemGroupData = cData.Descendants().FirstOrDefault(x => x.Name == E_ITEM_GROUP_DATA);

                if (itemGroupData != null)
                {
                    Context.ItemGroup = new ItemGroup(
                       itemGroupData.Attribute(A_ITEMGROUP_OID) == null ? "" : itemGroupData.Attribute(A_ITEMGROUP_OID).Value,
                       itemGroupData.Attribute(A_ITEMGROUP_REPEAT_KEY) == null ? 0 : TryInt(itemGroupData.Attribute(A_ITEMGROUP_REPEAT_KEY).Value),
                       itemGroupData.Attribute(A_TRANSACTION_TYPE) == null ? "" : itemGroupData.Attribute(A_TRANSACTION_TYPE).Value,
                       itemGroupData.Attribute(A_RECORD_ID) == null ? -1 : TryInt(itemGroupData.Attribute(A_RECORD_ID).Value)
                   );
                }

                //Item Data
                var itemData = cData.Descendants().FirstOrDefault(x => x.Name == E_ITEM_DATA);

                if (itemData != null)
                {
                    Context.Item = new Item(
                       itemData.Attribute(A_ITEM_OID) == null ? "" : itemData.Attribute(A_ITEM_OID).Value,
                       itemData.Attribute(A_VALUE) == null ? "" : itemData.Attribute(A_VALUE).Value,
                       TrueOrFalse(itemData.Attribute(A_FREEZE)),
                       TrueOrFalse(itemData.Attribute(A_VERIFY)),
                       TrueOrFalse(itemData.Attribute(A_LOCK)),
                       itemData.Attribute(A_TRANSACTION_TYPE) == null ? "" : itemData.Attribute(A_TRANSACTION_TYPE).Value
                   );
                }

                // Query Data
                var queryData = GetElementsFrom(cData, E_QUERY);

                if(queryData != null)
                {

                    Context.Query = new Query(
                        TryInt(GetAttributeValueFrom(queryData, A_QUERY_REPEAT_KEY)),
                        GetAttributeValueFrom(queryData, A_STATUS),
                        GetAttributeValueFrom(queryData, A_RESPONSE),
                        GetAttributeValueFrom(queryData, A_RECIPIENT),
                        GetAttributeValueFrom(queryData, A_VALUE)
                    );
                }

                // Protocol Deviation Data
                var pDevData = GetElementsFrom(cData, E_PROTOCOL_DEVIATION);

                if(pDevData != null)
                {
                    Context.ProtocolDeviation = new ProtocolDeviation(
                        TryInt(GetAttributeValueFrom(pDevData, A_PROTCOL_DEVIATION_REPEAT_KEY)),
                        GetAttributeValueFrom(pDevData, A_CODE),
                        GetAttributeValueFrom(pDevData, A_CLASS),
                        GetAttributeValueFrom(pDevData, A_STATUS),
                        GetAttributeValueFrom(pDevData, A_VALUE),
                        GetAttributeValueFrom(pDevData, A_TRANSACTION_TYPE)

                    );
                }

                //Review Data
                var reviewData = GetElementsFrom(cData, E_REVIEW);
                if(reviewData != null)
                {
                    Context.Review = new Review(
                        GetAttributeValueFrom(reviewData, A_GROUP_NAME),
                        TrueOrFalse(reviewData.Attribute(A_REVIEWED))
                    );
                }

                //Comment Data

                var commentData = GetElementsFrom(cData, E_COMMENT);
                if(commentData != null)
                {
                    Context.Comment = new Comment(
                          TryInt(GetAttributeValueFrom(commentData, A_COMMENT_REPEAT_KEY)),
                          GetAttributeValueFrom(commentData, A_VALUE),
                          GetAttributeValueFrom(commentData, A_TRANSACTION_TYPE)
                    );
                }

                //Signature Data
                var sigData = GetElementsFrom(cData, E_SIGNATURE);
                if(sigData != null)
                {
                    REF_STATE = SIGNATURE_REF_STATE;
                }

                //Signature Ref Data
                var sigRefData = GetElementsFrom(cData, E_SIGNATURE_REF);
                if(sigRefData != null)
                {
                    Context.Signature.OID = GetAttributeValueFrom(sigRefData, A_SIGNATURE_OID);
                }

                //Raise event for each context element built
                OnContextBuilt(Context);

            }
        }


        /// <summary>
        /// Return the integer representation of the source string. If the string cannot be
        /// be parsed, return a default.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="_default">The default.</param>
        /// <returns></returns>
        private int TryInt(string source, int _default = -1)
        {
            int output;
            if (int.TryParse(source, out output)) return output;
            return _default;

        }


        /// <summary>
        /// Return the value of an attribute of an element, or an empty string if it's null.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns></returns>
        private static string GetAttributeValueFrom(XElement element, XName attributeName)
        {
            return element.Attribute(attributeName) == null ? "" : element.Attribute(attributeName)?.Value;
                      
        }


        /// <summary>
        /// Check if the root element has descendants that match the given XName. 
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="attrib">The attribute.</param>
        /// <returns></returns>
        private static XElement GetElementsFrom(XContainer root, XName attrib)
        {
            return root.Descendants().FirstOrDefault(x => x.Name == attrib);
        }


        /// <summary>
        /// Returns the AuditRecord or Signature property of the context object based on
        /// </summary>
        /// <returns></returns>
        protected HasUserElements AuditOrSignatureElement()
        {
            if (REF_STATE == AUDIT_REF_STATE) return Context.AuditRecord;
            else return Context.Signature;
        }



        /// <summary>
        /// If "yes" then True, otherwise false.
        /// </summary>
        /// <param name="attrib">The attribute.</param>
        /// <returns></returns>
        private static bool TrueOrFalse(XAttribute attrib)
        {
            if (attrib == null) return false;
            return attrib.Value.ToLower() == "yes";
        }

        /// <summary>
        /// Gets the last source identifier.
        /// </summary>
        /// <returns></returns>
        public int GetLastSourceId()
        {
            return LastSourceID;
        }
    }

  
}
