using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Medidata.RWS.Core.Requests.ODM_Adapter;

namespace Medidata.RWS.Tests.Core.Requests.ODM_Adapter
{
    [TestClass]
    public class VersionFoldersRequestTests
    {
        [TestMethod]
        public void VersionFoldersRequest_can_properly_create_a_request_with_query_string_parameters()
        {

            var vfReq = new VersionFoldersRequest(ProjectName: "Mediflex", Environment: "Dev");

            Assert.IsTrue(vfReq.UrlPath().Contains("datasets/VersionFolders.odm"));

            Assert.IsTrue(vfReq.UrlPath().Contains(string.Format("studyoid={0}", vfReq.studyoid)));

        }
    }
}
