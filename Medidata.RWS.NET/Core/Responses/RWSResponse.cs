using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Medidata.RWS.Core.Responses
{

    /// <summary>
    /// Parses RWS Response messages.
    /// </summary>
    public class RWSResponse : RWSXMLResponse
    {
        /// <summary>
        /// The subjects touched
        /// </summary>
        public readonly int SubjectsTouched;
        /// <summary>
        /// The fields touched
        /// </summary>
        public readonly int FieldsTouched;
        /// <summary>
        /// The folders touched
        /// </summary>
        public readonly int FoldersTouched;
        /// <summary>
        /// The forms touched
        /// </summary>
        public readonly int FormsTouched;
        /// <summary>
        /// The inbound odm file oid
        /// </summary>
        public readonly string InboundODMFileOID;
        /// <summary>
        /// Whether or not the transaction is successful.
        /// </summary>
        public readonly bool IsTransactionSuccessful;
        /// <summary>
        /// The log lines touched
        /// </summary>
        public readonly int LogLinesTouched;
        /// <summary>
        /// The new records
        /// </summary>
        public readonly string NewRecords;
        /// <summary>
        /// The reference number
        /// </summary>
        public readonly string ReferenceNumber;


        private readonly string SuccessStats;

        /// <summary>
        /// The response object
        /// </summary>
        public readonly IRestResponse ResponseObject;


        /// <summary>
        /// Initializes a new instance of the <see cref="RWSResponse"/> class.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException"></exception>
        public RWSResponse(IRestResponse response) : base(response.Content)
        {

            ResponseObject = response;

            ReferenceNumber = rootNode.GetAttribute("ReferenceNumber");
            InboundODMFileOID = rootNode.GetAttribute("InboundODMFileOID");
            IsTransactionSuccessful = rootNode.GetAttribute("IsTransactionSuccessful") == "1";

            SubjectsTouched = 0;
            FoldersTouched = 0;
            FormsTouched = 0;
            FieldsTouched = 0;
            LogLinesTouched = 0;

            SuccessStats = rootNode.GetAttribute("SuccessStatistics");

            if(SuccessStats.StartsWith("Rave objects touched:"))
            {
                SuccessStats = SuccessStats.Substring("Rave objects touched:".Length + 1);

                var parts = SuccessStats.Split(';');

                foreach(var part in parts)
                {
                    string[] nameValues = part.Trim().Split('=');

                    var name = nameValues.ElementAt(0);
                    var value = nameValues.ElementAt(1);

                    switch(name)
                    {
                        case "Subjects":
                            SubjectsTouched = Convert.ToInt32(value);
                            break;
                        case "Folders":
                            FoldersTouched = Convert.ToInt32(value);
                            break;
                        case "Forms":
                            FormsTouched = Convert.ToInt32(value);
                            break;
                        case "Fields":
                            FieldsTouched = Convert.ToInt32(value);
                            break;
                        case "LogLines":
                            LogLinesTouched = Convert.ToInt32(value);
                            break;
                        default:
                            throw new KeyNotFoundException(string.Format("Unknown RAVE Object {0} in response {1}", name, SuccessStats));
                    }

                }

            }

            NewRecords = rootNode.GetAttribute("NewRecords");

        }

        /// <summary>
        /// Finds a FormData element given a form OID, a study event OID, and an itemData Value.
        /// Note: this assumes one instance of a form in the given folder only.
        /// </summary>
        /// <param name="formOid">The form oid.</param>
        /// <param name="studyEventOid">The study event oid.</param>
        /// <param name="itemDataOid">The item data oid.</param>
        /// <param name="itemDataValue">The item data value.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public XElement GetFormDataElementByContext(string formOid, string studyEventOid, string itemDataOid, string itemDataValue)
        {

            var firstInstanceForms = GetAllFirstInstanceForms(formOid);

            return (from formData in firstInstanceForms
                    select formData.Descendants(XNamespace.Get(Constants.ODM_NS) + "ItemData")
                        .FirstOrDefault(
                            el =>
                                el.Attribute("ItemOID") != null && el.Attribute("ItemOID")?.Value == string.Format("{0}.{1}", formOid, itemDataOid) &&
                                el.Attribute("Value") != null && el.Attribute("Value")?.Value == itemDataValue
                            ) 
                    into result where result != null
                    select result.Ancestors(XNamespace.Get(Constants.ODM_NS) + "FormData").FirstOrDefault()).FirstOrDefault();
        }

        /// <summary>
        /// Finds the first folder with a specific name.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        /// <returns></returns>
        public XElement FindFirstFolder(string folderName)
        {
            return GetFirstElementWithAttributeValue("StudyEventData", "StudyEventOID", folderName);
        }

        /// <summary>
        /// Finds all folders with a specific oid.
        /// </summary>
        /// <param name="Oid">The oid.</param>
        /// <returns></returns>
        public IEnumerable<XElement> FindAllFoldersWithOid(string Oid)
        {
            return GetAllElementsWithAttributeValue("StudyEventData", "StudyEventOID", Oid);
        }

        /// <summary>
        /// Finds the first form in a folder.
        /// </summary>
        /// <param name="formName">Name of the form.</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <returns></returns>
        public XElement FindFirstFormInFolder(string formName, string folderName)
        {
            var firstInstanceForms = GetAllFirstInstanceForms(formName);

            return firstInstanceForms.FirstOrDefault(el =>
            {
                //The parent in this case is the folder - e.g. StudyEventData
                var xAttribute = el.Parent?.Attribute("StudyEventOID");
                return xAttribute != null && (xAttribute.Value == folderName);
            });

        }

        /// <summary>
        /// Finds all forms with a specific oid.
        /// </summary>
        /// <param name="oid">The oid.</param>
        /// <returns></returns>
        public IEnumerable<XElement> GetAllFirstInstanceForms(string oid)
        {
            var forms = GetAllElementsWithAttributeValue("FormData", "FormOID", oid);

            return forms.Where(el =>
            {
                var xAttribute = el.Attribute("FormRepeatKey");
                return xAttribute != null && (xAttribute.Value == "1");
            });

        }


    }
}
