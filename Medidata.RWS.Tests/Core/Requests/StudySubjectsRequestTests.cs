using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Medidata.RWS.Core.Requests.Implementations;
using RestSharp;
using Moq;
using Medidata.RWS.Core.RWSObjects;
using System.Linq;
using Medidata.RWS.Core;
using Medidata.RWS.Schema;

namespace Medidata.RWS.Tests.Core.Requests
{
    [TestClass]
    public class StudySubjectsRequestTests
    {

        private string ProjectName = "A Project";
        private string _Envrionment = "DEV";

        [TestInitialize]
        public void SetUp()
        {

        }

        [TestMethod]
        public void StudySubjectsRequest_validates_subject_key_type()
        {
            try
            {
                var request = new StudySubjectsRequest(ProjectName, _Envrionment, subjectKeyType: "BetaMax");
            } catch(NotSupportedException ex)
            {
                Assert.AreEqual(ex.Message, "SubjectKeyType BetaMax is not a valid value");
            }

            var request2 = new StudySubjectsRequest(ProjectName, _Envrionment, subjectKeyType: "SubjectName");
            Assert.AreEqual(ProjectName, request2.ProjectName);

        }



        [TestMethod]
        public void StudySubjectsRequest_can_request_with_UUID()
        {

            var request = new StudySubjectsRequest(ProjectName, _Envrionment, subjectKeyType: "SubjectUUID");
            Assert.IsTrue(request.UrlPath().Contains("subjectKeyType=SubjectUUID"));

        }


        [TestMethod]
        public void StudySubjectsRequest_can_omit_subject_key_type()
        {

            var request = new StudySubjectsRequest(ProjectName, _Envrionment);
            Assert.IsFalse(request.UrlPath().Contains("subjectKeyType"));

        }


        [TestMethod]
        public void StudySubjectsRequest_can_request_status()
        {
            var request = new StudySubjectsRequest(ProjectName, _Envrionment, status: true);

            Assert.IsTrue(request.UrlPath().Contains("status=all"));

        }


        [TestMethod]
        public void StudySubjectsRequest_correctly_requests_with_include_parameter_when_provided()
        {
            StudySubjectsRequest request;
            request = new StudySubjectsRequest(ProjectName, _Envrionment, include: "inactive");
            Assert.IsTrue(request.UrlPath().Contains("include=inactive"));
            request = new StudySubjectsRequest(ProjectName, _Envrionment, include: "inactiveAndDeleted");
            Assert.IsTrue(request.UrlPath().Contains("include=inactiveAndDeleted"));
            request = new StudySubjectsRequest(ProjectName, _Envrionment, include: "deleted");
            Assert.IsTrue(request.UrlPath().Contains("include=deleted"));

            try
            {
                request = new StudySubjectsRequest(ProjectName, _Envrionment, include: "All_da_things");
            }
            catch (NotSupportedException ex)
            {
                Assert.AreEqual(ex.Message, "If provided, `include` must be one of the following: inactive,deleted,inactiveAndDeleted");
            }

        }


        [TestMethod]
        public void StudySubjectsRequest_correctly_parses_response()
        {
  
            string response_data =
                @"<ODM FileType=""Snapshot"" FileOID=""767a1f8b-7b72-4d12-adbe-37d4d62ba75e""
                         CreationDateTime=""2013-04-08T10:02:17.781-00:00""
                         ODMVersion=""1.3""
                         xmlns:mdsol=""http://www.mdsol.com/ns/odm/metadata""
                         xmlns:xlink=""http://www.w3.org/1999/xlink""
                         xmlns=""http://www.cdisc.org/ns/odm/v1.3"">
                 <ClinicalData StudyOID=""FakeItTillYaMakeIt(Dev)"" MetaDataVersionOID=""1111"">
                    <SubjectData SubjectKey=""000002"">
                       <SiteRef LocationOID=""101""/>
                    </SubjectData>
                 </ClinicalData>
                 <ClinicalData StudyOID=""FakeItTillYaMakeIt(Dev)"" MetaDataVersionOID=""1111"">
                     <SubjectData SubjectKey=""000003"">
                        <SiteRef LocationOID=""6""/>
                     </SubjectData>
                 </ClinicalData>
                 <ClinicalData StudyOID=""FakeItTillYaMakeIt(Dev)"" MetaDataVersionOID=""1111"">
                     <SubjectData SubjectKey=""EC82F1AB-D463-4930-841D-36FC865E63B2"" mdsol:SubjectName=""1"" mdsol:SubjectKeyType=""SubjectUUID"">
                        <SiteRef LocationOID=""6""/>
                     </SubjectData>
                 </ClinicalData>
            </ODM>";


            var request = new StudySubjectsRequest(ProjectName, _Envrionment);

            var mockResponse = new Mock<IRestResponse>();
            mockResponse.Setup(x => x.Content).Returns(response_data);

            var response = request.Result(mockResponse.Object) as RWSSubjects;

            Assert.IsInstanceOfType(response, typeof(RWSSubjects));

            var subjectNames = new string[] { "000002", "000003", "1" };

            foreach (var subject in response)
            {
                Assert.IsInstanceOfType(subject, typeof(RWSSubjectListItem));
                Assert.IsTrue(subjectNames.Contains(subject.SubjectName));
            }


        }


    }
}
