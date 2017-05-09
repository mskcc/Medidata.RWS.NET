using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Medidata.RWS.Core.Requests.Implementations;

namespace Medidata.RWS.Tests.Core.Requests
{
    [TestClass]
    public class StudyVersionRequestTests
    {
        [TestMethod]
        public void StudyVersionRequest_computes_URL_correctly()
        {
            var req = new StudyVersionRequest(ProjectName: "FakeItTillYaMakeIt(Dev)", OID: "1234");
            Assert.AreEqual(1234, req.OID);
            Assert.AreEqual("metadata/studies/FakeItTillYaMakeIt(Dev)/versions/1234", req.UrlPath());
        }

    }
}
