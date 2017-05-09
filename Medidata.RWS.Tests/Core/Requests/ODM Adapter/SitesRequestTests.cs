using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Medidata.RWS.Core.Requests.ODM_Adapter;

namespace Medidata.RWS.Tests.Core.Requests.ODM_Adapter
{
    [TestClass]
    public class SitesRequestTests
    {
        [TestMethod]
        public void SitesRequest_can_properly_create_a_request_with_query_string_parameters()
        {

            var sitesReq = new SitesRequest(ProjectName: "Mediflex", Environment: "DEV");

            Assert.AreEqual(string.Format("datasets/Sites.odm?studyoid={0}", sitesReq.studyoid.Trim()), sitesReq.UrlPath());

            sitesReq = new SitesRequest();

            Assert.AreEqual(string.Format("datasets/Sites.odm"), sitesReq.UrlPath());

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SitesRequest_must_include_an_environment_if_projectname_is_supplied()
        {

            var sitesReq = new SitesRequest(ProjectName: "Mediflex", Environment: "");


        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SitesRequest_must_include_a_projectname_if_environment_is_supplied()
        {

            var sitesReq = new SitesRequest(ProjectName: "", Environment: "PROD");


        }

    }
}
