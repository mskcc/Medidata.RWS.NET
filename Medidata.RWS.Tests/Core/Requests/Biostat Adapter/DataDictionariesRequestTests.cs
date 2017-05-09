using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Medidata.RWS.Core.Requests.Biostat_Adapter;

namespace Medidata.RWS.Tests.Core.Requests.Biostat_Adapter
{
    [TestClass]
    public class DataDictionariesRequestTests
    {
        [TestMethod]
        public void DataDictionariesRequest_constructs_the_correct_UrlPath()
        {

            var req = new DataDictionariesRequest("Mediflex", "DEV");

            Assert.AreEqual(req.UrlPath(), "datasets/SDTMDataDictionaries.csv?studyid=Mediflex(DEV)");

            var req2 = new DataDictionariesRequest("Mediflex", "DEV", datasetFormat: "xml");

            Assert.AreEqual(req2.UrlPath(), "datasets/SDTMDataDictionaries?studyid=Mediflex(DEV)");

        }
    }
}
