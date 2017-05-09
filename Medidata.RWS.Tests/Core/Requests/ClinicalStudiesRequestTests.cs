using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Medidata.RWS.Core.Requests.Implementations;
using Moq;
using Medidata.RWS.Core.RWSObjects;
using RestSharp;
using System.Linq;

namespace Medidata.RWS.Tests.Core.Requests
{
    [TestClass]
    public class ClinicalStudiesRequestTests
    {
        [TestMethod]
        public void ClinicalStudiesRequest_computes_URL_correctly()
        {
            var req = new ClinicalStudiesRequest();
            Assert.AreEqual("studies", req.UrlPath());
        }


        [TestMethod]
        public void ClinicalStudiesRequest_can_format_response_properly()
        {

            string response_data =
                @"<ODM FileType=""Snapshot"" FileOID=""767a1f8b-7b72-4d12-adbe-37d4d62ba75e""
                         CreationDateTime=""2013-04-08T10:02:17.781-00:00""
                         ODMVersion=""1.3""
                         xmlns:mdsol=""http://www.mdsol.com/ns/odm/metadata""
                         xmlns:xlink=""http://www.w3.org/1999/xlink""
                         xmlns=""http://www.cdisc.org/ns/odm/v1.3"">
                         <Study OID=""FakeItTillYaMakeIt(Dev)"">
                            <GlobalVariables>
                                  <StudyName>FakeItTillYaMakeIt(Dev)</StudyName>
                                  <StudyDescription/>
                                  <ProtocolName>FakeItTillYaMakeIt</ProtocolName>
                            </GlobalVariables>
                         </Study>
                         <Study OID=""Mediflex(Prod)"">
                            <GlobalVariables>
                                  <StudyName>Mediflex(Prod)</StudyName>
                                  <StudyDescription/>
                                  <ProtocolName>Mediflex</ProtocolName>
                            </GlobalVariables>
                         </Study>
                    </ODM>";

            var request = new MetadataStudiesRequest();

            var mockResponse = new Mock<IRestResponse>();
            mockResponse.Setup(x => x.Content).Returns(response_data);

            var response = request.Result(mockResponse.Object) as RWSStudies;

            Assert.IsInstanceOfType(response, typeof(RWSStudies));

            var studyOIDS = new string[] { "Mediflex(Prod)", "FakeItTillYaMakeIt(Dev)" };
            var environments = new string[] { "Prod", "Dev" };

            foreach (var study in response)
            {
                Assert.IsTrue(studyOIDS.Contains(study.OID));

            }
        }
    }
}
