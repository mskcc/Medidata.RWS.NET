using System;
using Medidata.RWS.Core.Requests.Datasets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Medidata.RWS.Tests.Core.Requests.Datasets
{
    [TestClass]
    public class VersionDatasetRequestTests
    {
        [TestMethod]
        public void VersionDatasetRequest_default_version_path_is_correct()
        {
            var request = new VersionDatasetRequest(project_name: "Mediflex", environment_name: "Dev", version_oid: "001");

            Assert.AreEqual("Mediflex", request.ProjectName);

            Assert.AreEqual("Dev", request.EnvironmentName);

            Assert.AreEqual("001", request.VersionOid);

            Assert.AreEqual("studies/Mediflex(Dev)/versions/001/datasets/regular", request.UrlPath());

        }

        [TestMethod]
        public void VersionDatasetRequest_raw_version_path_is_correct()
        {
            var request = new VersionDatasetRequest(project_name: "Mediflex", environment_name: "Dev", version_oid: "001", dataset_type:"raw");

            Assert.AreEqual("Mediflex", request.ProjectName);

            Assert.AreEqual("Dev", request.EnvironmentName);

            Assert.AreEqual("001", request.VersionOid);

            Assert.AreEqual("studies/Mediflex(Dev)/versions/001/datasets/raw", request.UrlPath());

        }

        [TestMethod]
        public void VersionDatasetRequest_raw_version_path_with_form_is_correct()
        {
            var request = new VersionDatasetRequest(project_name: "Mediflex", environment_name: "Dev", version_oid: "001", dataset_type: "raw", formOid: "DM");

            Assert.AreEqual("Mediflex", request.ProjectName);

            Assert.AreEqual("Dev", request.EnvironmentName);

            Assert.AreEqual("001", request.VersionOid);

            Assert.AreEqual("studies/Mediflex(Dev)/versions/001/datasets/raw/DM", request.UrlPath());

        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void VersionDatasetRequest_throws_with_improper_parameters()
        {

            var req = new VersionDatasetRequest("TESTPROJECT", "DEV", "1001", dataset_type: "newfie");

        }


    }
}
