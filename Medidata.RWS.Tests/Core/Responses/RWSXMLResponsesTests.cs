using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Medidata.RWS.Core.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;

namespace Medidata.RWS.Tests.Core.Responses
{
    [TestClass]
    public class RWSXMLResponsesTests
    {

        [TestMethod]
        public void RWSXMLResponse_can_return_first_element_with_specific_attribute_value()
        {
            var mockResponse = new Mock<IRestResponse>();
            mockResponse.Setup(x => x.Content).Returns(Response);

            var resp = new RWSResponse(mockResponse.Object);

            var c1D1Folder = resp.GetFirstElementWithAttributeValue("StudyEventData", "StudyEventOID", "C1D1");
            var repeatkey = c1D1Folder.Attribute("StudyEventRepeatKey")?.Value;

            Assert.IsNotNull(c1D1Folder);
            Assert.AreEqual("C1D1", c1D1Folder.Attribute("StudyEventOID")?.Value);

            Assert.IsNotNull(repeatkey);
            Assert.AreEqual("C1[1]/C1D1[1]", repeatkey);
        }


        [TestMethod]
        public void RWSXMLResponse_can_return_all_elements_with_specific_attribute_value()
        {
            var mockResponse = new Mock<IRestResponse>();

            mockResponse.Setup(x => x.Content).Returns(Response);

            var resp = new RWSResponse(mockResponse.Object);

            var folders = resp.GetAllElementsWithAttributeValue("StudyEventData", "StudyEventOID", "C1D1");

            Assert.IsNotNull(folders);

            var fs = folders as IList<XElement> ?? folders.ToList();

            Assert.AreEqual(2, fs.Count());
            foreach (var f in fs)
            {
                Assert.AreEqual("C1D1", f.Attribute("StudyEventOID")?.Value);
            }
        }



        [TestMethod]
        public void RWSXMLResponse_can_parse_invalid_characters()
        {
            //Arrange
            var testResponse = new TestResponse();

            //Act 
            testResponse.ParseXMLString(InvalidXMLResponse);

            //Assert
            //No exceptions means we successfully parsed the string

        }



        /// <summary>
        /// Test class for testing abstract RWSXMLResponse.
        /// </summary>
        /// <seealso cref="Medidata.RWS.Core.Responses.RWSXMLResponse" />
        private class TestResponse : RWSXMLResponse
        {
        }

        private const string InvalidXMLResponse = @"<?xml version=""1.0"" encoding=""utf-8""?>
                <ODM FileType=""Snapshot"" FileOID=""69c36236-9c95-4a23-9420-cfe94f831c8d"" CreationDateTime=""2016-11-20T21:33:00.884-00:00"" ODMVersion=""1.3"" xmlns:mdsol=""http://www.mdsol.com/ns/odm/metadata"" xmlns:xlink=""http://www.w3.org/1999/xlink"" xmlns=""http://www.cdisc.org/ns/odm/v1.3"">
                    <ClinicalData StudyOID=""Mediflex(DEV3 LabTest)"" MetaDataVersionOID=""812"">
                        <SubjectData SubjectKey=""MJK"">
                            <SiteRef LocationOID=""101"" />
                            <StudyEventData StudyEventOID=""LABS"" StudyEventRepeatKey=""LAB[1]/LABS[1]"">
                                <FormData FormOID=""HEM"" FormRepeatKey=""1"">
                                    <ItemGroupData ItemGroupOID=""HEM_LOG_LINE"">
                                        <ItemData ItemOID=""HEM.HEMDATE"" Value=""#x9"" />
                                        <ItemData ItemOID=""HEM.HEMDATE2"" Value=""#xA"" />
                                        <ItemData ItemOID=""HEM.HEMDATE3"" Value=""#xD"" />
                                        <ItemData ItemOID=""HEM.HEMDATE4"" Value=""&#x3;"" />
                                        <ItemData ItemOID=""HEM.HEMDATE5"" Value=""#x20"" />
                                        <ItemData ItemOID=""HEM.HEMDATE6"" Value=""#xD7FF"" />
                                        <ItemData ItemOID=""HEM.HEMDATE7"" Value=""#xE000"" />
                                        <ItemData ItemOID=""HEM.HEMDATE8"" Value=""#xFFFD"" />
                                        <ItemData ItemOID=""HEM.HEMDATE8"" Value=""#x10000"" />
                                        <ItemData ItemOID=""HEM.HEMDATE8"" Value=""#x10FFFF"" />
                                    </ItemGroupData>
                                </FormData>
                            </StudyEventData>
                        </SubjectData>
                    </ClinicalData>
                </ODM>";


        private const string Response = @"<?xml version=""1.0"" encoding=""utf-8""?>
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
                            <StudyEventData StudyEventOID=""C1D1"" StudyEventRepeatKey=""C1[1]/C1D1[2]"">
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
