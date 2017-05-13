using System;
using System.Text;
using System.Collections.Generic;
using System.Threading;
using Medidata.RWS.Core.Requests;
using Medidata.RWS.Core.Requests.ODM_Adapter;
using Medidata.RWS.Core.Responses;
using Medidata.RWS.Extras.AuditEvent;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;

namespace Medidata.RWS.Tests.Extras.AuditEvent
{
    /// <summary>
    /// Summary description for AuditEventParserTests
    /// </summary>
    [TestClass]
    public class AuditEventParserTests
    {
     

        [TestMethod]
        public void AuditEventParser_Parses_Subject_Data()
        {

            var mockResponse = new Mock<IRestResponse>();

            mockResponse.Setup(x => x.Content).Returns(AuditRecordXML);

            var odmAdapterResponse = new Mock<RWSResponse>(mockResponse.Object);

            odmAdapterResponse.Setup(x => x.RawXMLString()).Returns(AuditRecordXML);

            var conn = new Mock<IRWSConnection>();

            var mockParser = new AuditEventParser();

            conn.Setup(m => m.SendRequest(
                It.IsAny<AuditRecordsRequest>(), null)).Returns(odmAdapterResponse.Object);

            var odmAdapter = new OdmAdapter(conn.Object, mockParser, "", "", new TestSubscriber());

            odmAdapter.Run(1, -1, 1000);

        }

        private string AuditRecordXML = @"<ODM ODMVersion=""1.3"" FileType=""Transactional"" FileOID=""7b6914bd-ab20-421c-b6f5-bdfb3e5bf3b1"" CreationDateTime=""2016-10-14T19:07:40"" xmlns=""http://www.cdisc.org/ns/odm/v1.3"" xmlns:mdsol=""http://www.mdsol.com/ns/odm/metadata"">
                     <ClinicalData StudyOID=""Mediflex(DEV3 LabTest)"" MetaDataVersionOID =""812""  mdsol:AuditSubCategoryName=""SubjectCreated"">
                            <SubjectData SubjectKey=""c550986b-496f-4389-9ddb-cbdffdf5e525"" mdsol:SubjectKeyType=""SubjectUUID"" mdsol:SubjectName=""002"" TransactionType=""Upsert"">
                                <SiteRef LocationOID=""101"" />
                                  <AuditRecord>
                                    <UserRef UserOID=""systemuser""/>
                                    <LocationRef LocationOID=""101""/>
                                    <DateTimeStamp>2013-04-26T19:31:06</DateTimeStamp>
                                    <ReasonForChange></ReasonForChange>
                                    <SourceID>8253891</SourceID>
                                </AuditRecord>
                            </SubjectData>
                        </ClinicalData>
                </ODM>";

        public class TestSubscriber : AuditEventSubscriber
        {

            public override void OnContextBuilt(object source, ContextEventArgs e)
            {
                Assert.AreEqual("c550986b-496f-4389-9ddb-cbdffdf5e525", e.Context.Subject.Key);
                Assert.AreEqual("SubjectUUID", e.Context.Subject.SubjectKeyType);
                Assert.AreEqual("002", e.Context.Subject.Name);
                Assert.AreEqual("Upsert", e.Context.Subject.TransactionType);
            }   

            public override void OnPageProcessed(object source, EventArgs args)
            {
            }

            public override void OnParsingComplete(object source, ODMAdapterEventArgs args)
            {
               
            }
        }
    }
}
