using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Medidata.RWS.Core.Responses;
using RestSharp;
using Moq;

namespace Medidata.RWS.Tests.Core.Responses
{
    [TestClass]
    public class RWSResponseTests
    {
        [TestMethod]
        public void RWSResponse_correctly_reads_a_RWS_response()
        {

            const string response = @"<Response ReferenceNumber=""82e942b0-48e8-4cf4-b299-51e2b6a89a1b""
                    InboundODMFileOID=""""
                    IsTransactionSuccessful=""1""
                    SuccessStatistics=""Rave objects touched: Subjects=1; Folders=2; Forms=3; Fields=4; LogLines=5"" NewRecords="""">
             </Response>";

            var mockResponse = new Mock<IRestResponse>();
            mockResponse.Setup(x => x.Content).Returns(response);

            var resp = new RWSResponse(mockResponse.Object);

            Assert.AreEqual("82e942b0-48e8-4cf4-b299-51e2b6a89a1b", resp.ReferenceNumber);
            Assert.IsTrue(resp.IsTransactionSuccessful);
            Assert.AreEqual(1, resp.SubjectsTouched);
            Assert.AreEqual(2, resp.FoldersTouched);
            Assert.AreEqual(3, resp.FormsTouched);
            Assert.AreEqual(4, resp.FieldsTouched);
            Assert.AreEqual(5, resp.LogLinesTouched);

        }

        [TestMethod]
        public void RWSResponse_can_find_the_first_instance_of_a_form_contained_in_a_folder()
        {

            var mockResponse = new Mock<IRestResponse>();
            mockResponse.Setup(x => x.Content).Returns(DatasetResponse);

            var resp = new RWSResponse(mockResponse.Object);

            var firstInstance = resp.FindFirstFormInFolder("HEM", "C1D1");

            Assert.IsNotNull(firstInstance);
            Assert.IsNotNull(firstInstance.Parent);
            Assert.AreEqual("1", firstInstance.Attribute("FormRepeatKey")?.Value);
            Assert.AreEqual("HEM", firstInstance.Attribute("FormOID")?.Value);
            Assert.AreEqual("C1D1", firstInstance.Parent?.Attribute("StudyEventOID")?.Value);
            Assert.AreEqual("C1[1]/C1D1[1]", firstInstance.Parent?.Attribute("StudyEventRepeatKey")?.Value);

        }


        [TestMethod]
        public void RWSResponse_can_find_a_FormData_element_given_a_FormOID_a_StudyEventOID_and_an_ItemData_value()
        {
            var mockResponse = new Mock<IRestResponse>();

            mockResponse.Setup(x => x.Content).Returns(ComplexLabResults);

            var resp = new RWSResponse(mockResponse.Object);
            
            var formDataInstance = resp.GetFormDataElementByContext("LAB1", "CYCLEXDAY1", "INSTANCE_ID", "168151");

            Assert.IsNotNull(formDataInstance);
            Assert.IsNotNull(formDataInstance.Parent);
            Assert.AreEqual("1", formDataInstance.Attribute("FormRepeatKey")?.Value);
            Assert.AreEqual("LAB1", formDataInstance.Attribute("FormOID")?.Value);
            Assert.AreEqual("CYCLEXDAY1", formDataInstance.Parent?.Attribute("StudyEventOID")?.Value);
            Assert.AreEqual("CYCLE[4]/CYCLEXDAY1[1]", formDataInstance.Parent?.Attribute("StudyEventRepeatKey")?.Value);

        }

        [TestMethod]
        public void RWSResponse_can_find_the_first_instance_of_a_folder()
        {

            var mockResponse = new Mock<IRestResponse>();
            mockResponse.Setup(x => x.Content).Returns(DatasetResponse);

            var resp = new RWSResponse(mockResponse.Object);

            var firstInstance = resp.FindFirstFolder("C1D1");
  
            Assert.IsNotNull(firstInstance);
            Assert.AreEqual("C1D1", firstInstance.Attribute("StudyEventOID")?.Value);
            Assert.AreEqual("C1[1]/C1D1[1]", firstInstance.Attribute("StudyEventRepeatKey")?.Value);

        }

        [TestMethod] public void RWSResponse_can_find_all_folders_with_specific_oid()
        {

            var mockResponse = new Mock<IRestResponse>();

            mockResponse.Setup(x => x.Content).Returns(DatasetResponse);

            var resp = new RWSResponse(mockResponse.Object);

            var allFolders = resp.FindAllFoldersWithOid("C1D1");

            Assert.IsNotNull(allFolders);

            Assert.AreEqual(3, allFolders.Count());

        }

        [TestMethod]
        public void RWSResponse_can_find_all_first_instance_forms()
        {

            var mockResponse = new Mock<IRestResponse>();

            mockResponse.Setup(x => x.Content).Returns(DatasetResponse);

            var resp = new RWSResponse(mockResponse.Object);

            var forms = resp.GetAllFirstInstanceForms("HEM");

            Assert.IsNotNull(forms);

            var fs = forms as IList<XElement> ?? forms.ToList();
            Assert.AreEqual(3, fs.Count());

            foreach (var f in fs)
            {
                Assert.AreEqual("1", f.Attribute("FormRepeatKey")?.Value);
            }


        }

        private const string ComplexLabResults = @"<?xml version=""1.0"" encoding=""utf-8""?>
<ODM FileType=""Snapshot"" FileOID=""9284197d-2007-442f-b639-6f21b3b33de9"" CreationDateTime=""2017-01-06T16:01:34.400-00:00"" ODMVersion=""1.3"" xmlns:mdsol=""http://www.mdsol.com/ns/odm/metadata"" xmlns:xlink=""http://www.w3.org/1999/xlink"" xmlns=""http://www.cdisc.org/ns/odm/v1.3"">
    <ClinicalData StudyOID=""Mediflex(DEV2 LabTest)"" MetaDataVersionOID=""1236"">
        <SubjectData SubjectKey=""GAMMA02"">
            <SiteRef LocationOID=""101"" />
            <StudyEventData StudyEventOID=""CYCLEXDAY1"" StudyEventRepeatKey=""CYCLE[1]/CYCLEXDAY1[1]"">
                <FormData FormOID=""LAB1"" FormRepeatKey=""1"">
                    <ItemGroupData ItemGroupOID=""LAB1_LOG_LINE"">
                        <ItemData ItemOID=""LAB1.OTHERLABDATE"" Value=""2016-02-08T11:39:00"" />
                    </ItemGroupData>
                </FormData>
            </StudyEventData>
        </SubjectData>
    </ClinicalData>
    <ClinicalData StudyOID=""Mediflex(DEV2 LabTest)"" MetaDataVersionOID=""1236"">
        <SubjectData SubjectKey=""GAMMA02"">
            <SiteRef LocationOID=""101"" />
            <StudyEventData StudyEventOID=""CYCLEXDAY1"" StudyEventRepeatKey=""CYCLE[2]/CYCLEXDAY1[1]"">
                <FormData FormOID=""LAB1"" FormRepeatKey=""1"">
                    <ItemGroupData ItemGroupOID=""LAB1_LOG_LINE"">
                        <ItemData ItemOID=""LAB1.OTHERLABDATE"" Value=""2016-04-06T09:59:00"" />
                    </ItemGroupData>
                </FormData>
            </StudyEventData>
        </SubjectData>
    </ClinicalData>
    <ClinicalData StudyOID=""Mediflex(DEV2 LabTest)"" MetaDataVersionOID=""1236"">
        <SubjectData SubjectKey=""GAMMA02"">
            <SiteRef LocationOID=""101"" />
            <StudyEventData StudyEventOID=""CYCLEXDAY3"" StudyEventRepeatKey=""CYCLE[2]/CYCLEXDAY3[1]"">
                <FormData FormOID=""LAB1"" FormRepeatKey=""1"">
                    <ItemGroupData ItemGroupOID=""LAB1_LOG_LINE"">
                        <ItemData ItemOID=""LAB1.OTHERLABDATE"" Value=""2016-04-06T10:24:00"" />
                    </ItemGroupData>
                </FormData>
            </StudyEventData>
        </SubjectData>
    </ClinicalData>
    <ClinicalData StudyOID=""Mediflex(DEV2 LabTest)"" MetaDataVersionOID=""1236"">
        <SubjectData SubjectKey=""GAMMA02"">
            <SiteRef LocationOID=""101"" />
            <StudyEventData StudyEventOID=""CYCLEXDAY1"" StudyEventRepeatKey=""CYCLE[3]/CYCLEXDAY1[1]"">
                <FormData FormOID=""LAB1"" FormRepeatKey=""1"">
                    <ItemGroupData ItemGroupOID=""LAB1_LOG_LINE"">
                        <ItemData ItemOID=""LAB1.OTHERLABDATE"" Value=""2016-04-06"" />
                    </ItemGroupData>
                </FormData>
            </StudyEventData>
        </SubjectData>
    </ClinicalData>
    <ClinicalData StudyOID=""Mediflex(DEV2 LabTest)"" MetaDataVersionOID=""1236"">
        <SubjectData SubjectKey=""GAMMA02"">
            <SiteRef LocationOID=""101"" />
            <StudyEventData StudyEventOID=""CYCLEXDAY3"" StudyEventRepeatKey=""CYCLE[3]/CYCLEXDAY3[1]"">
                <FormData FormOID=""LAB1"" FormRepeatKey=""1"">
                    <ItemGroupData ItemGroupOID=""LAB1_LOG_LINE"">
                        <ItemData ItemOID=""LAB1.OTHERLABDATE"" Value=""2016-04-06T18:15:00"" />
                    </ItemGroupData>
                </FormData>
            </StudyEventData>
        </SubjectData>
    </ClinicalData>
    <ClinicalData StudyOID=""Mediflex(DEV2 LabTest)"" MetaDataVersionOID=""1236"">
        <SubjectData SubjectKey=""GAMMA02"">
            <SiteRef LocationOID=""101"" />
            <StudyEventData StudyEventOID=""CYCLEXDAY1"" StudyEventRepeatKey=""CYCLE[4]/CYCLEXDAY1[1]"">
                <FormData FormOID=""LAB1"" FormRepeatKey=""1"">
                    <ItemGroupData ItemGroupOID=""LAB1_LOG_LINE"">
                        <ItemData ItemOID=""LAB1.OTHERLABDATE"" Value=""2016-04-04T15:31:00"" />
                        <ItemData ItemOID=""LAB1.INSTANCE_ID"" Value=""168151"" />
                    </ItemGroupData>
                </FormData>
            </StudyEventData>
        </SubjectData>
    </ClinicalData>
    <ClinicalData StudyOID=""Mediflex(DEV2 LabTest)"" MetaDataVersionOID=""1236"">
        <SubjectData SubjectKey=""GAMMA02"">
            <SiteRef LocationOID=""101"" />
            <StudyEventData StudyEventOID=""CYCLEXDAY3"" StudyEventRepeatKey=""CYCLE[4]/CYCLEXDAY3[1]"">
                <FormData FormOID=""LAB1"" FormRepeatKey=""1"">
                    <ItemGroupData ItemGroupOID=""LAB1_LOG_LINE"">
                        <ItemData ItemOID=""LAB1.OTHERLABDATE"" Value=""2016-04-04T15:30:00"" />
                    </ItemGroupData>
                </FormData>
            </StudyEventData>
        </SubjectData>
    </ClinicalData>
</ODM>";

        private const string DatasetResponse = @"<?xml version=""1.0"" encoding=""utf-8""?>
                <ODM FileType=""Snapshot"" FileOID=""69c36236-9c95-4a23-9420-cfe94f831c8d"" CreationDateTime=""2016-11-20T21:33:00.884-00:00"" ODMVersion=""1.3"" xmlns:mdsol=""http://www.mdsol.com/ns/odm/metadata"" xmlns:xlink=""http://www.w3.org/1999/xlink"" xmlns=""http://www.cdisc.org/ns/odm/v1.3"">
                    <ClinicalData StudyOID=""Mediflex(DEV3 LabTest)"" MetaDataVersionOID=""812"">
                        <SubjectData SubjectKey=""MJK"">
                            <SiteRef LocationOID=""101"" />
                            <StudyEventData StudyEventOID=""LABS"" StudyEventRepeatKey=""LAB[1]/LABS[1]"">
                                <FormData FormOID=""HEM"" FormRepeatKey=""1"">
                                    <ItemGroupData ItemGroupOID=""HEM_LOG_LINE"">
                                        <ItemData ItemOID=""HEM.FAKE_DATE"" IsNull=""Yes"" />
                                        <ItemData ItemOID=""HEM.HEM_COM"" IsNull=""Yes"" />
                                        <ItemData ItemOID=""HEM.HEMDATE"" Value=""2015-05-27"" />
                                    </ItemGroupData>
                                </FormData>
                            </StudyEventData>
                        </SubjectData>
                    </ClinicalData>
                    <ClinicalData StudyOID=""Mediflex(DEV3 LabTest)"" MetaDataVersionOID=""812"">
                        <SubjectData SubjectKey=""MJK"">
                            <SiteRef LocationOID=""101"" />
                            <StudyEventData StudyEventOID=""C1D1"" StudyEventRepeatKey=""C1[1]/C1D1[1]"">
                                <FormData FormOID=""HEM"" FormRepeatKey=""1"">
                                    <ItemGroupData ItemGroupOID=""HEM_LOG_LINE"">
                                        <ItemData ItemOID=""HEM.FAKE_DATE"" IsNull=""Yes"" />
                                        <ItemData ItemOID=""HEM.HEM_COM"" IsNull=""Yes"" />
                                        <ItemData ItemOID=""HEM.HEMDATE"" Value=""2015-05-26"" />
                                    </ItemGroupData>
                                </FormData>
                            </StudyEventData>
                        </SubjectData>
                    </ClinicalData>
                    <ClinicalData StudyOID=""Mediflex(DEV3 LabTest)"" MetaDataVersionOID=""812"">
                        <SubjectData SubjectKey=""MJK"">
                            <SiteRef LocationOID=""101"" />
                            <StudyEventData StudyEventOID=""C1D1"" StudyEventRepeatKey=""C1[1]/C1D1[1]"">
                                <FormData FormOID=""HEM"" FormRepeatKey=""2"">
                                    <ItemGroupData ItemGroupOID=""HEM_LOG_LINE"">
                                        <ItemData ItemOID=""HEM.FAKE_DATE"" IsNull=""Yes"" />
                                        <ItemData ItemOID=""HEM.HEM_COM"" IsNull=""Yes"" />
                                        <ItemData ItemOID=""HEM.HEMDATE"" Value=""2015-05-26"" />
                                    </ItemGroupData>
                                </FormData>
                            </StudyEventData>
                        </SubjectData>
                    </ClinicalData>
                    <ClinicalData StudyOID=""Mediflex(DEV3 LabTest)"" MetaDataVersionOID=""812"">
                        <SubjectData SubjectKey=""MJK"">
                            <SiteRef LocationOID=""101"" />
                            <StudyEventData StudyEventOID=""C1D1"" StudyEventRepeatKey=""C1[1]/C1D1[1]"">
                                <FormData FormOID=""HEM"" FormRepeatKey=""3"">
                                    <ItemGroupData ItemGroupOID=""HEM_LOG_LINE"">
                                        <ItemData ItemOID=""HEM.FAKE_DATE"" IsNull=""Yes"" />
                                        <ItemData ItemOID=""HEM.HEM_COM"" IsNull=""Yes"" />
                                        <ItemData ItemOID=""HEM.HEMDATE"" Value=""2015-05-26"" />
                                    </ItemGroupData>
                                </FormData>
                            </StudyEventData>
                        </SubjectData>
                    </ClinicalData>
                    <ClinicalData StudyOID=""Mediflex(DEV3 LabTest)"" MetaDataVersionOID=""812"">
                        <SubjectData SubjectKey=""MJK"">
                            <SiteRef LocationOID=""101"" />
                            <StudyEventData StudyEventOID=""C1D8"" StudyEventRepeatKey=""C1[1]/C1D8[1]"">
                                <FormData FormOID=""HEM"" FormRepeatKey=""1"">
                                    <ItemGroupData ItemGroupOID=""HEM_LOG_LINE"">
                                        <ItemData ItemOID=""HEM.FAKE_DATE"" IsNull=""Yes"" />
                                        <ItemData ItemOID=""HEM.HEM_COM"" IsNull=""Yes"" />
                                        <ItemData ItemOID=""HEM.HEMDATE"" Value=""2015-05-26"" />
                                    </ItemGroupData>
                                </FormData>
                            </StudyEventData>
                        </SubjectData>
                    </ClinicalData>
                </ODM>";
    }
}
