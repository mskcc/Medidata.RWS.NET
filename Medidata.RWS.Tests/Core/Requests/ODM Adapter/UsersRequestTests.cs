using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Medidata.RWS.Core.Requests.ODM_Adapter;

namespace Medidata.RWS.Tests.Core.Requests.ODM_Adapter
{
    [TestClass]
    public class UsersRequestTests
    {
        [TestMethod]
        public void UsersRequest_can_properly_create_a_request_with_query_string_parameters()
        {

            var uRequest = new UsersRequest(ProjectName: "Mediflex", Environment: "Dev");

            Assert.IsTrue(uRequest.UrlPath().Contains("datasets/Users.odm"));

            Assert.IsTrue(uRequest.UrlPath().Contains(string.Format("studyoid={0}", uRequest.studyoid)));

            uRequest = new UsersRequest(ProjectName: "Mediflex", Environment: "Dev", locationoid: "101");

            Assert.IsTrue(uRequest.UrlPath().Contains(string.Format("studyoid={0}", uRequest.studyoid)));

            Assert.IsTrue(uRequest.UrlPath().Contains(string.Format("locationoid={0}", uRequest.locationoid)));



        }
    }
}
