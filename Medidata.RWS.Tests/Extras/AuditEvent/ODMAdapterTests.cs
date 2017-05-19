using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Medidata.RWS.Core.Requests;
using Medidata.RWS.Extras.AuditEvent;
using System.Threading;
using Moq;
using Medidata.RWS.Core.Responses;
using RestSharp;
using Medidata.RWS.Core.Requests.ODM_Adapter;

namespace Medidata.RWS.Tests.Extras.AuditEvent
{

    public class DummyDBWarehouseService : AuditEventSubscriber
    {

        public override void OnContextBuilt(object source, ContextEventArgs e)
        {

            Console.WriteLine(e.Context.MetadataVersion);
            Thread.Sleep(1000);

        }

        public override void OnPageProcessed(object source, EventArgs args)
        {
            Thread.Sleep(1000);
        }

        public override void OnParsingComplete(object source, ODMAdapterEventArgs args)
        {
            Console.WriteLine(args.LastSourceID);
            Thread.Sleep(1000);
        }
    }


    [TestClass]
    public class ODMAdapterTests
    {
        
        [TestInitialize()]
        public void Initialize()
        {



        }

        [TestMethod]
        public void ODMAdapter_can_be_successfully_instantiated()
        {

            var conn = new RwsConnection("innovate", "fakeuser", "fakepass");

            var odm = new OdmAdapter(conn, new AuditEventParser(), "FAKESTUDY", "DEV3 LabTest");

            //no exceptions means we pass.
        }


        [TestMethod]
        public void ODMAdapter_Parser_properly_flattens_xml_response()
        {


            var mockResponse = new Mock<IRestResponse>();

            mockResponse.Setup(x => x.Content).Returns(AuditRecordXML);

            var odmAdapterResponse = new Mock<RWSResponse>(mockResponse.Object);

            odmAdapterResponse.Setup(x => x.RawXMLString()).Returns(AuditRecordXML);

            var conn = new Mock<IRWSConnection>();

            conn.Setup(m => m.SendRequest(
                It.IsAny<AuditRecordsRequest>(), null)).Returns(odmAdapterResponse.Object);

            var odmAdapter = new Mock<OdmAdapter>(conn.Object, new AuditEventParser(), "", "");
            
            odmAdapter.Object.Run(1, -1, 1000);

            //No exceptions means the odm Adapter ran successfully

        }

        [TestMethod]
        public void ODMAdapter_can_raise_events_and_subscribers_are_notified()
        {

            var mockResponse = new Mock<IRestResponse>();

            mockResponse.Setup(x => x.Content).Returns(AuditRecordXML);

            var odmAdapterResponse = new Mock<RWSResponse>(mockResponse.Object);

            odmAdapterResponse.Setup(x => x.RawXMLString()).Returns(AuditRecordXML);

            var conn = new Mock<IRWSConnection>();

            var mockParser = new AuditEventParser();

            var dummySubscriber = new Mock<DummyDBWarehouseService>();

            conn.Setup(m => m.SendRequest(
                It.IsAny<AuditRecordsRequest>(), null)).Returns(odmAdapterResponse.Object);

            var odmAdapter = new OdmAdapter(conn.Object, mockParser, "", "", dummySubscriber.Object);

            odmAdapter.Run(1, -1, 1000);

            dummySubscriber.Verify(x => x.OnContextBuilt(mockParser, It.IsAny<ContextEventArgs>()), Times.Exactly(7));

            dummySubscriber.Verify(x => x.OnParsingComplete(odmAdapter, It.IsAny<ODMAdapterEventArgs>()), Times.Once());

            dummySubscriber.Verify(x => x.OnPageProcessed(odmAdapter, It.IsAny<EventArgs>()), Times.Once());

            //We verified the subscriber was invoked as needed.

        }


        [TestMethod]
        public void ODMAdapter_contains_reference_to_correct_last_source_id()
        {


            var mockResponse = new Mock<IRestResponse>();

            mockResponse.Setup(x => x.Content).Returns(AuditRecordXML);

            var odmAdapterResponse = new Mock<RWSResponse>(mockResponse.Object);

            odmAdapterResponse.Setup(x => x.RawXMLString()).Returns(AuditRecordXML);

            var conn = new Mock<IRWSConnection>();

            var mockParser = new AuditEventParser();

            conn.Setup(m => m.SendRequest(
                It.IsAny<AuditRecordsRequest>(), null)).Returns(odmAdapterResponse.Object);

            var odmAdapter = new OdmAdapter(conn.Object, mockParser, "", "");

            odmAdapter.Run(1, -1, 1000);

            Assert.AreEqual(8253891, odmAdapter.LastSourceId);

        }



        [TestMethod]
        public void ODMAdapter_can_get_link_and_rel_from_response_headers()
        {

            var mockResponse = new Mock<IRestResponse>();

            mockResponse.Setup(x => x.Content).Returns(AuditRecordXML);

            mockResponse.Setup(x => x.Headers).Returns(new List<Parameter>(){ new Parameter { Name = "Link", Value = "<https://innovate.mdsol.com/RaveWebServices/datasets/ClinicalAuditRecords.odm?studyoid=Mediflex&per_page=10000&startid=8253885>; rel=\"next\"" } });

            var odmAdapterResponse = new Mock<RWSResponse>(mockResponse.Object);

            odmAdapterResponse.Setup(x => x.RawXMLString()).Returns(AuditRecordXML);

            var conn = new Mock<IRWSConnection>();

            var mockParser = new AuditEventParser();

            conn.Setup(m => m.SendRequest(
                It.IsAny<AuditRecordsRequest>(), null)).Returns(odmAdapterResponse.Object);

            conn.Setup(m => m.GetLastResult()).Returns(mockResponse.Object);

            var odmAdapter = new OdmAdapter(conn.Object, mockParser, "", "");

            odmAdapter.Run(1, 1, 1000); 

            Assert.AreEqual("https://innovate.mdsol.com/RaveWebServices/datasets/ClinicalAuditRecords.odm?studyoid=Mediflex&per_page=10000&startid=8253885", odmAdapter.LastLink);
            Assert.AreEqual("next", odmAdapter.LastRel);

        }


        private string AuditRecordXML = @"<ODM ODMVersion=""1.3"" FileType=""Transactional"" FileOID=""7b6914bd-ab20-421c-b6f5-bdfb3e5bf3b1"" CreationDateTime=""2016-10-14T19:07:40"" xmlns=""http://www.cdisc.org/ns/odm/v1.3"" xmlns:mdsol=""http://www.mdsol.com/ns/odm/metadata"">
	                <ClinicalData StudyOID=""Mediflex(DEV3 LabTest)"" MetaDataVersionOID=""812""  mdsol:AuditSubCategoryName=""SubjectCreated"">
		                <SubjectData SubjectKey=""199127a1-1061-4e26-9f45-adf0b26b7c90"" mdsol:SubjectKeyType=""SubjectUUID"" mdsol:SubjectName=""fff""  TransactionType=""Upsert""  >
			                <AuditRecord>
				                <UserRef UserOID=""kochm1""/>
				                <LocationRef LocationOID=""101""/>
				                <DateTimeStamp>2016-02-19T14:01:05</DateTimeStamp>
				                <ReasonForChange/>
				                <SourceID>8253885</SourceID>
			                </AuditRecord>
			                <SiteRef LocationOID=""101"" />
		                </SubjectData>
	                </ClinicalData>
	                <ClinicalData StudyOID=""Mediflex(DEV3 LabTest)"" MetaDataVersionOID=""812""  mdsol:AuditSubCategoryName=""Entered"">
		                <SubjectData SubjectKey=""199127a1-1061-4e26-9f45-adf0b26b7c90"" mdsol:SubjectKeyType=""SubjectUUID"" mdsol:SubjectName=""fff""  >
			                <SiteRef LocationOID=""101"" />
			                <StudyEventData StudyEventOID=""SUBJECT""  >
				                <FormData FormOID=""SUBJECT"" FormRepeatKey=""1""  mdsol:DataPageId=""293607"" >
					                <ItemGroupData ItemGroupOID=""SUBJECT""   mdsol:RecordId=""633213"" >
						                <ItemData ItemOID=""SUBJECT.SUBJECT"" TransactionType=""Upsert""  Value=""fff"" 	 >
							                <AuditRecord>
								                <UserRef UserOID=""kochm1""/>
								                <LocationRef LocationOID=""101""/>
								                <DateTimeStamp>2016-02-19T14:01:05</DateTimeStamp>
								                <ReasonForChange/>
								                <SourceID>8253886</SourceID>
							                </AuditRecord>
						                </ItemData>
					                </ItemGroupData>
				                </FormData>
			                </StudyEventData>
		                </SubjectData>
	                </ClinicalData>
	                <ClinicalData StudyOID=""Mediflex(DEV3 LabTest)"" MetaDataVersionOID=""812""  mdsol:AuditSubCategoryName=""SubjectNameChanged"">
		                <SubjectData SubjectKey=""199127a1-1061-4e26-9f45-adf0b26b7c90"" mdsol:SubjectKeyType=""SubjectUUID"" mdsol:SubjectName=""fff""  TransactionType=""Upsert""  >
			                <AuditRecord>
				                <UserRef UserOID=""systemuser""/>
				                <LocationRef LocationOID=""101""/>
				                <DateTimeStamp>2016-02-19T14:01:05</DateTimeStamp>
				                <ReasonForChange/>
				                <SourceID>8253887</SourceID>
			                </AuditRecord>
			                <SiteRef LocationOID=""101"" />
		                </SubjectData>
	                </ClinicalData>
                     <ClinicalData StudyOID=""Mediflex(DEV3 LabTest)"" MetaDataVersionOID =""812""  mdsol:AuditSubCategoryName=""ClinicalSignificanceEmpty"">
                            <SubjectData SubjectKey=""c550986b-496f-4389-9ddb-cbdffdf5e525"" mdsol:SubjectKeyType=""SubjectUUID"" mdsol:SubjectName=""002""  >
                                <SiteRef LocationOID=""101"" />
                                <StudyEventData StudyEventOID=""DAY8""  StudyEventRepeatKey=""INDCYCLE[1]/DAY8[1]""   mdsol:InstanceId=""11009"" >
                                    <FormData FormOID=""HEM"" FormRepeatKey=""1""  mdsol:DataPageId=""62031"" >
                                        <ItemGroupData ItemGroupOID=""HEM""   mdsol:RecordId=""103092"" >
                                            <ItemData ItemOID=""HEM.LUC"" TransactionType=""Upsert""  >
                                                <AuditRecord>
                                                    <UserRef UserOID=""systemuser""/>
                                                    <LocationRef LocationOID=""101""/>
                                                    <DateTimeStamp>2013-04-26T19:31:06</DateTimeStamp>
                                                    <ReasonForChange></ReasonForChange>
                                                    <SourceID>8253888</SourceID>
                                                </AuditRecord>
                                                <mdsol:ClinicalSignificance Value="""" Comment="""" TransactionType=""Upsert""/>
                                            </ItemData>
                                        </ItemGroupData>
                                    </FormData>
                                </StudyEventData>
                            </SubjectData>
                        </ClinicalData>
                     <ClinicalData StudyOID=""Mediflex(DEV3 LabTest)"" MetaDataVersionOID =""812""  mdsol:AuditSubCategoryName=""ClinicalSignificanceEmpty"">
                            <SubjectData SubjectKey=""c550986b-496f-4389-9ddb-cbdffdf5e525"" mdsol:SubjectKeyType=""SubjectUUID"" mdsol:SubjectName=""002""  >
                                <SiteRef LocationOID=""101"" />
                                <StudyEventData StudyEventOID=""DAY8""  StudyEventRepeatKey=""INDCYCLE[1]/DAY8[1]""   mdsol:InstanceId=""11009"" >
                                    <FormData FormOID=""HEM"" FormRepeatKey=""1""  mdsol:DataPageId=""62031"" >
                                        <ItemGroupData ItemGroupOID=""HEM""   mdsol:RecordId=""103092"" >
                                            <ItemData ItemOID=""HEM.LUC"" TransactionType=""Upsert""  >
                                                <AuditRecord>
                                                    <UserRef UserOID=""systemuser""/>
                                                    <LocationRef LocationOID=""101""/>
                                                    <DateTimeStamp>2013-04-26T19:31:06</DateTimeStamp>
                                                    <ReasonForChange></ReasonForChange>
                                                    <SourceID>8253889</SourceID>
                                                </AuditRecord>
                                                <mdsol:ClinicalSignificance Value="""" Comment="""" TransactionType=""Upsert""/>
                                            </ItemData>
                                        </ItemGroupData>
                                    </FormData>
                                </StudyEventData>
                            </SubjectData>
                        </ClinicalData>
                     <ClinicalData StudyOID=""Mediflex(DEV3 LabTest)"" MetaDataVersionOID =""812""  mdsol:AuditSubCategoryName=""ClinicalSignificanceEmpty"">
                            <SubjectData SubjectKey=""c550986b-496f-4389-9ddb-cbdffdf5e525"" mdsol:SubjectKeyType=""SubjectUUID"" mdsol:SubjectName=""002""  >
                                <SiteRef LocationOID=""101"" />
                                <StudyEventData StudyEventOID=""DAY8""  StudyEventRepeatKey=""INDCYCLE[1]/DAY8[1]""   mdsol:InstanceId=""11009"" >
                                    <FormData FormOID=""HEM"" FormRepeatKey=""1""  mdsol:DataPageId=""62031"" >
                                        <ItemGroupData ItemGroupOID=""HEM""   mdsol:RecordId=""103092"" >
                                            <ItemData ItemOID=""HEM.LUC"" TransactionType=""Upsert""  >
                                                <AuditRecord>
                                                    <UserRef UserOID=""systemuser""/>
                                                    <LocationRef LocationOID=""101""/>
                                                    <DateTimeStamp>2013-04-26T19:31:06</DateTimeStamp>
                                                    <ReasonForChange></ReasonForChange>
                                                    <SourceID>8253890</SourceID>
                                                </AuditRecord>
                                                <mdsol:ClinicalSignificance Value="""" Comment="""" TransactionType=""Upsert""/>
                                            </ItemData>
                                        </ItemGroupData>
                                    </FormData>
                                </StudyEventData>
                            </SubjectData>
                        </ClinicalData>
                     <ClinicalData StudyOID=""Mediflex(DEV3 LabTest)"" MetaDataVersionOID =""812""  mdsol:AuditSubCategoryName=""ClinicalSignificanceEmpty"">
                            <SubjectData SubjectKey=""c550986b-496f-4389-9ddb-cbdffdf5e525"" mdsol:SubjectKeyType=""SubjectUUID"" mdsol:SubjectName=""002""  >
                                <SiteRef LocationOID=""101"" />
                                <StudyEventData StudyEventOID=""DAY8""  StudyEventRepeatKey=""INDCYCLE[1]/DAY8[1]""   mdsol:InstanceId=""11009"" >
                                    <FormData FormOID=""HEM"" FormRepeatKey=""1""  mdsol:DataPageId=""62031"" >
                                        <ItemGroupData ItemGroupOID=""HEM""   mdsol:RecordId=""103092"" >
                                            <ItemData ItemOID=""HEM.LUC"" TransactionType=""Upsert""  >
                                                <AuditRecord>
                                                    <UserRef UserOID=""systemuser""/>
                                                    <LocationRef LocationOID=""101""/>
                                                    <DateTimeStamp>2013-04-26T19:31:06</DateTimeStamp>
                                                    <ReasonForChange></ReasonForChange>
                                                    <SourceID>8253891</SourceID>
                                                </AuditRecord>
                                                <mdsol:ClinicalSignificance Value="""" Comment="""" TransactionType=""Upsert""/>
                                            </ItemData>
                                        </ItemGroupData>
                                    </FormData>
                                </StudyEventData>
                            </SubjectData>
                        </ClinicalData>
                </ODM>";

    }





    
}
