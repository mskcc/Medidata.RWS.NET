using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Medidata.RWS.Core.Requests.ODM_Adapter;

namespace Medidata.RWS.Tests.Core.Requests.ODM_Adapter
{
    [TestClass]
    public class AuditRecordsRequestTests
    {
        [TestMethod]
        public void AuditRecordsRequest_can_properly_create_a_request_with_query_string_parameters()
        {

            var aRequest = new AuditRecordsRequest(ProjectName: "MediFlex", Environment: "Dev", startid: 1, per_page: 100);
          
            Assert.IsTrue(aRequest.UrlPath().Contains("datasets/ClinicalAuditRecords.odm"));

            Assert.IsTrue(aRequest.UrlPath().Contains(string.Format("studyoid={0}", aRequest.studyoid)));

            Assert.IsTrue(aRequest.UrlPath().Contains("startid=1"));

            Assert.IsTrue(aRequest.UrlPath().Contains("per_page=100"));

            aRequest = new AuditRecordsRequest(ProjectName: "MediFlex", Environment: "Dev", startid: 2596, per_page: 45);

            Assert.IsTrue(aRequest.UrlPath().Contains("startid=2596"));

            Assert.IsTrue(aRequest.UrlPath().Contains("per_page=45"));
        }
    }
}
