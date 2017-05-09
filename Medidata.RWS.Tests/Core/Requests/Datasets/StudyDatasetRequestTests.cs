using System;
using Medidata.RWS.Core.Requests.Datasets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Medidata.RWS.Tests.Core.Requests.Datasets
{
    [TestClass]
    public class StudyDatasetRequestTests
    {
        [TestMethod]
        public void StudyDatasetRequest_default_parameters_are_set_correctly()
        {

            var req = new StudyDatasetRequest("TESTPROJECT", "DEV");

            Assert.AreEqual("TESTPROJECT", req.ProjectName);

            Assert.AreEqual("DEV", req.EnvironmentName);

            Assert.AreEqual("studies/TESTPROJECT(DEV)/datasets/regular", req.UrlPath());

        }

        [TestMethod]
        public void StudyDatasetRequest_correctly_configured_requests_raw_dataset()
        {

            var req = new StudyDatasetRequest("TESTPROJECT", "DEV", dataset_type:"raw");

            Assert.AreEqual("TESTPROJECT", req.ProjectName);

            Assert.AreEqual("DEV", req.EnvironmentName);

            Assert.AreEqual("studies/TESTPROJECT(DEV)/datasets/raw", req.UrlPath());

        }


        [TestMethod]
        public void StudyDatasetRequest_correctly_configured_for_raw_dataset_with_rawsuffix()
        {

            var req = new StudyDatasetRequest("TESTPROJECT", "DEV", dataset_type: "raw", rawsuffix: "RX");

            Assert.AreEqual("TESTPROJECT", req.ProjectName);

            Assert.AreEqual("DEV", req.EnvironmentName);

            Assert.AreEqual("studies/TESTPROJECT(DEV)/datasets/raw?rawsuffix=RX", req.UrlPath());

        }


        [TestMethod]
        public void StudyDatasetRequest_correctly_configured_for_raw_dataset_with_formOid()
        {

            var req = new StudyDatasetRequest("TESTPROJECT", "DEV", dataset_type: "raw", formOid: "DM");

            Assert.AreEqual("TESTPROJECT", req.ProjectName);

            Assert.AreEqual("DEV", req.EnvironmentName);

            Assert.AreEqual("studies/TESTPROJECT(DEV)/datasets/raw/DM", req.UrlPath());

        }


        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void StudyDatasetRequest_throws_with_improper_parameters()
        {

            var req = new StudyDatasetRequest("TESTPROJECT", "DEV", dataset_type: "newfie");

        }

    }
}
