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

        private Mock<IRWSConnection> connection;
        private Mock<IRestResponse> response;
        private Mock<RWSResponse> odmAdapterResponse;

        [TestMethod]
        public void AuditEventParser_Parses_ClinicalData_Data()
        {

            response = new Mock<IRestResponse>();
            response.Setup(x => x.Content).Returns(QueryAnsweredXML);
            odmAdapterResponse = new Mock<RWSResponse>(response.Object);
            odmAdapterResponse.Setup(x => x.RawXMLString()).Returns(QueryAnsweredXML);
            connection = new Mock<IRWSConnection>();
            connection.Setup(m => m.SendRequest(
                 It.IsAny<AuditRecordsRequest>(), null)).Returns(odmAdapterResponse.Object);

            var mockParser = new AuditEventParser();

            var odmAdapter = new OdmAdapter(connection.Object, mockParser, "", "", new ClinicalDataSubscriber());

            odmAdapter.Run(1, -1, 1000);

        }


        [TestMethod]
        public void AuditEventParser_Parses_Subject_TransactionType()
        {

            response = new Mock<IRestResponse>();
            response.Setup(x => x.Content).Returns(SubjectCreatedXML);
            odmAdapterResponse = new Mock<RWSResponse>(response.Object);
            odmAdapterResponse.Setup(x => x.RawXMLString()).Returns(SubjectCreatedXML);
            connection = new Mock<IRWSConnection>();
            connection.Setup(m => m.SendRequest(
                 It.IsAny<AuditRecordsRequest>(), null)).Returns(odmAdapterResponse.Object);

            var mockParser = new AuditEventParser();

            var odmAdapter = new OdmAdapter(connection.Object, mockParser, "", "", new SubjectDataSubscriber());

            odmAdapter.Run(1, -1, 1000);

        }





        private string SubjectCreatedXML = @"<ODM ODMVersion=""1.3"" FileType=""Transactional"" FileOID=""7b6914bd-ab20-421c-b6f5-bdfb3e5bf3b1"" CreationDateTime=""2016-10-14T19:07:40"" xmlns=""http://www.cdisc.org/ns/odm/v1.3"" xmlns:mdsol=""http://www.mdsol.com/ns/odm/metadata"">
                     <ClinicalData StudyOID=""Mediflex(DEV3 LabTest)"" MetaDataVersionOID =""812""  mdsol:AuditSubCategoryName=""SubjectCreated"">
                            <SubjectData SubjectKey=""1725b509-596b-4c0a-a8bc-1f7ccf34fb1a"" mdsol:SubjectKeyType=""SubjectUUID"" mdsol:SubjectName=""002"" TransactionType=""Upsert"">
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



        private string QueryAnsweredXML = @"<ODM ODMVersion=""1.3"" FileType=""Transactional"" FileOID=""7b6914bd-ab20-421c-b6f5-bdfb3e5bf3b1"" CreationDateTime=""2016-10-14T19:07:40"" xmlns=""http://www.cdisc.org/ns/odm/v1.3"" xmlns:mdsol=""http://www.mdsol.com/ns/odm/metadata"">
                <ClinicalData StudyOID=""Mediflex(DEV3 LabTest)"" MetaDataVersionOID=""812""  mdsol:AuditSubCategoryName=""QueryAnswerByChange"">
                        <SubjectData SubjectKey=""1725b509-596b-4c0a-a8bc-1f7ccf34fb1a"" mdsol:SubjectKeyType=""SubjectUUID"" mdsol:SubjectName=""007"">
                            <SiteRef LocationOID=""101"" />
                            <StudyEventData StudyEventOID=""SCREEN""  StudyEventRepeatKey=""SCREEN[1]""   mdsol:InstanceId=""12345"" TransactionType=""Update"">
                                <FormData FormOID=""DEMOGRAPHICS"" FormRepeatKey=""1""  mdsol:DataPageId=""54321"" TransactionType=""Update"" >
                                    <ItemGroupData ItemGroupOID=""DEMOGRAPHICS""   mdsol:RecordId=""67890"" ItemGroupRepeatKey=""2"" TransactionType=""Update"">
                                        <ItemData ItemOID=""DEMOGRAPHICS.RACE"" TransactionType=""Upsert"" Value=""Test"" >
                                            <AuditRecord>
                                                <UserRef UserOID=""systemuser""/>
                                                <LocationRef LocationOID=""101""/>
                                                <DateTimeStamp>2013-05-14T18:25:24</DateTimeStamp>
                                                <ReasonForChange>Test Reason</ReasonForChange>
                                                <SourceID>1002343</SourceID>
                                            </AuditRecord>
                                            <MeasurementUnitRef MeasurementUnitOID=""111.VS Units TEMP.C""/>
                                            <mdsol:Query  QueryRepeatKey=""9001""  Value=""Data is required.  Please complete."" Status=""Answered"" Response=""1"" Recipient=""Site from System""/>
                                        </ItemData>
                                    </ItemGroupData>
                                </FormData>
                            </StudyEventData>
                        </SubjectData>
                    </ClinicalData>
                </ODM>";

        public class SubjectDataSubscriber : AuditEventSubscriber
        {

            public override void OnContextBuilt(object source, ContextEventArgs e)
            {

                Assert.AreEqual("Mediflex(DEV3 LabTest)", e.Context.StudyOID);
                Assert.AreEqual(812, e.Context.MetadataVersion);
                Assert.AreEqual("SubjectCreated", e.Context.SubCategory);
                Assert.AreEqual("1725b509-596b-4c0a-a8bc-1f7ccf34fb1a", e.Context.Subject.Key);
                Assert.AreEqual("SubjectUUID", e.Context.Subject.SubjectKeyType);
                Assert.AreEqual("002", e.Context.Subject.Name);
                Assert.AreEqual("Upsert", e.Context.Subject.TransactionType);
                Assert.AreEqual("101", e.Context.Subject.SiteRef.LocationOID);
            }   

            public override void OnPageProcessed(object source, EventArgs args)
            {
            }

            public override void OnParsingComplete(object source, ODMAdapterEventArgs args)
            {
            }
        }

        public class ClinicalDataSubscriber : AuditEventSubscriber
        {

            public override void OnContextBuilt(object source, ContextEventArgs e)
            {
                //StudyEvent
                Assert.AreEqual("SCREEN", e.Context.StudyEvent.OID);
                Assert.AreEqual("SCREEN[1]", e.Context.StudyEvent.StudyEventRepeatKey);
                Assert.AreEqual(12345, e.Context.StudyEvent.InstanceID);
                Assert.AreEqual("Update", e.Context.StudyEvent.TransactionType);

                //FormData
                Assert.AreEqual("DEMOGRAPHICS", e.Context.Form.OID);
                Assert.AreEqual("1", e.Context.Form.RepeatKey);
                Assert.AreEqual("Update", e.Context.Form.TransactionType);
                Assert.AreEqual(54321, e.Context.Form.DataPageId);

                //ItemGroup
                Assert.AreEqual("DEMOGRAPHICS", e.Context.ItemGroup.OID);
                Assert.AreEqual("2", e.Context.ItemGroup.RepeatKey);
                Assert.AreEqual("Update", e.Context.ItemGroup.TransactionType);
                Assert.AreEqual(67890, e.Context.ItemGroup.RecordId);

                //ItemData
                Assert.AreEqual("DEMOGRAPHICS.RACE", e.Context.Item.OID);
                Assert.AreEqual("Upsert", e.Context.Item.TransactionType);
                Assert.AreEqual("Test", e.Context.Item.Value);

                //AuditRecord
                Assert.AreEqual("systemuser", e.Context.AuditRecord.UserOID);
                Assert.AreEqual("101", e.Context.AuditRecord.LocationOID);
                Assert.AreEqual(DateTime.Parse("2013-05-14T18:25:24"), e.Context.AuditRecord.DateTimeStamp);
                Assert.AreEqual("Test Reason", e.Context.AuditRecord.ReasonForChange);
                Assert.AreEqual(1002343, e.Context.AuditRecord.SourceID);

                //Query
                Assert.AreEqual(9001, e.Context.Query.RepeatKey);
                Assert.AreEqual("Data is required.  Please complete.", e.Context.Query.Value);
                Assert.AreEqual("Answered", e.Context.Query.Status);
                Assert.AreEqual("1", e.Context.Query.Response);
                Assert.AreEqual("Site from System", e.Context.Query.Recipient);

                //MeasurementUnitOID
                Assert.AreEqual("111.VS Units TEMP.C", e.Context.Item.MeasurementUnitRef.MeasurementUnitOID);

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
