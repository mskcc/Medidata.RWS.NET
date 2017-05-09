using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Medidata.RWS.Core.Requests.Implementations;
using RestSharp;
using Moq;
using Medidata.RWS.Core.RWSObjects;
using System.Linq;

namespace Medidata.RWS.Tests.Core.Requests
{
    [TestClass]
    public class StudyDraftsRequestTests
    {
        [TestMethod]
        public void StudyDraftsRequest_computes_URL_correctly()
        {
            var req = new StudyDraftsRequest(ProjectName: "FakeItTillYaMakeIt(Dev)");

            Assert.AreEqual("metadata/studies/FakeItTillYaMakeIt(Dev)/drafts", req.UrlPath());
        }


        [TestMethod]
        public void StudyDraftsRequest_can_format_response_properly()
        {

            string response_data =
                @"<ODM ODMVersion=""1.3"" Granularity=""Metadata"" FileType=""Snapshot""
                FileOID=""d26b4d33-376d-4037-9747-684411190179""
                CreationDateTime=""2013-04-08T01:29:13""
                xmlns=""http://www.cdisc.org/ns/odm/v1.3""
                xmlns:mdsol=""http://www.mdsol.com/ns/odm/metadata"">
                    <Study OID=""Mediflex"">
                        <GlobalVariables>
                            <StudyName>Mediflex</StudyName>
                            <StudyDescription></StudyDescription>
                            <ProtocolName>Mediflex</ProtocolName>
                        </GlobalVariables>
                        <MetaDataVersion OID=""1213"" Name=""1.0_DRAFT"" />
                        <MetaDataVersion OID=""1194"" Name=""CF_TEST_DRAFT1"" />
                        <MetaDataVersion OID=""1164"" Name=""Initial"" />
                    </Study>
                </ODM>";

            var request = new StudyDraftsRequest(ProjectName: "FakeItTillYaMakeIt(Dev)");

            var mockResponse = new Mock<IRestResponse>();
            mockResponse.Setup(x => x.Content).Returns(response_data);

            var response = request.Result(mockResponse.Object) as RWSStudyMetadataVersions;

            Assert.IsInstanceOfType(response, typeof(RWSStudyMetadataVersions));

            Assert.AreEqual(response.Study.OID, "Mediflex");

            var versionOIDS = new string[] { "1213", "1194", "1164" };
            var versionNames = new string[] { "1.0_DRAFT", "CF_TEST_DRAFT1", "Initial" };

            foreach (var version in response)
            {
                Assert.IsTrue(versionOIDS.Contains(version.OID));
                Assert.IsTrue(versionNames.Contains(version.Name));
            }
        }
    }
}
