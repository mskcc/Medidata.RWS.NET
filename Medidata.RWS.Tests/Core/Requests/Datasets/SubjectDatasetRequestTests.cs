using System;
using Medidata.RWS.Core.Requests.Datasets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Medidata.RWS.Tests.Core.Requests.Datasets
{
    [TestClass]
    public class SubjectDatasetRequestTests
    {
        [TestMethod]
        public void SubjectDatasetRequest_default_parameters_are_set_correctly()
        {

            var req = new SubjectDatasetRequest("TESTPROJECT", "DEV", "1001");

            Assert.AreEqual("TESTPROJECT", req.ProjectName);

            Assert.AreEqual("DEV", req.EnvironmentName);

            Assert.AreEqual("1001", req.SubjectKey);

            Assert.AreEqual("studies/TESTPROJECT(DEV)/subjects/1001/datasets/regular", req.UrlPath());

        }

        [TestMethod]
        public void StudyDatasetRequest_correctly_configured_requests_raw_dataset()
        {

            var req = new SubjectDatasetRequest("TESTPROJECT", "DEV", "1001", dataset_type:"raw");

            Assert.AreEqual("TESTPROJECT", req.ProjectName);

            Assert.AreEqual("DEV", req.EnvironmentName);

            Assert.AreEqual("1001", req.SubjectKey);

            Assert.AreEqual("studies/TESTPROJECT(DEV)/subjects/1001/datasets/raw", req.UrlPath());

        }


        [TestMethod]
        public void StudyDatasetRequest_correctly_configured_for_raw_dataset_with_rawsuffix()
        {

            var req = new SubjectDatasetRequest("TESTPROJECT", "DEV", "1001", dataset_type: "raw", rawsuffix: "RX");

            Assert.AreEqual("TESTPROJECT", req.ProjectName);

            Assert.AreEqual("DEV", req.EnvironmentName);

            Assert.AreEqual("1001", req.SubjectKey);

            Assert.AreEqual("studies/TESTPROJECT(DEV)/subjects/1001/datasets/raw?rawsuffix=RX", req.UrlPath());

        }


        [TestMethod]
        public void StudyDatasetRequest_correctly_configured_for_raw_dataset_with_formOid()
        {

            var req = new SubjectDatasetRequest("TESTPROJECT", "DEV", "1001", dataset_type: "raw", formOid: "DM");

            Assert.AreEqual("TESTPROJECT", req.ProjectName);

            Assert.AreEqual("DEV", req.EnvironmentName);

            Assert.AreEqual("1001", req.SubjectKey);

            Assert.AreEqual("studies/TESTPROJECT(DEV)/subjects/1001/datasets/raw/DM", req.UrlPath());

        }


        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void StudyDatasetRequest_throws_with_improper_parameters()
        {

            var req = new SubjectDatasetRequest("TESTPROJECT", "DEV", "1001", dataset_type: "newfie");

        }

    }
}
