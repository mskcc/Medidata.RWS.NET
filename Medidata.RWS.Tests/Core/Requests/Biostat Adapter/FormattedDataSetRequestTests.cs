using System;
using Medidata.RWS.Core.Requests;
using Medidata.RWS.Core.Requests.Biostat_Adapter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Medidata.RWS.Core.Responses;
using RestSharp;

namespace Medidata.RWS.Tests.Core.Requests.Biostat_Adapter
{
    [TestClass]
    public class FormattedDataSetRequestTests
    {
 
        [TestMethod]
        public void FormattedDataSetRequest_allows_xml_and_csv_dataset_formats()
        {

            var req = new TestFormattedRequest("TEST PROJECT", "DEV");

            req.GetFormatExtension("CSV");
            req.GetFormatExtension("csv");
            req.GetFormatExtension("XML");
            req.GetFormatExtension("xml");

        }

        [TestMethod]
        public void FormattedDataSetRequest_does_not_allow_erroneous_dataset_formats()
        {

            var req = new TestFormattedRequest("TEST PROJECT", "DEV");

            string[] formatsToTry = {"xls", "xlsx", "doc"};

            foreach (var format in formatsToTry)
            {
                try
                {
                    req.GetFormatExtension(format);
                }
                catch (NotSupportedException e)
                {
                    Assert.IsTrue(e.Message.Contains($"`{format}` is not valid."));
                }

            }

        }

        [TestMethod]
        public void FormattedDataSetRequest_returns_appropriate_response_based_on_dataset_format()
        {

            var resp = new Mock<IRestResponse>();
            
            var req1 = new DataDictionariesRequest("Mediflex", "DEV");

            Assert.IsInstanceOfType(req1.Result(resp.Object), typeof(RWSTextResponse));

            var req2 = new DataDictionariesRequest("Mediflex", "DEV", "xml");

            Assert.IsInstanceOfType(req2.Result(resp.Object), typeof(RWSResponse));


        }

        private class TestFormattedRequest : FormattedDataSetRequest
        {
            public TestFormattedRequest(string projectName, string environmentName, string datasetFormat = "csv") : base(projectName, environmentName, datasetFormat)
            {


            }

            public override string UrlPath()
            {
                throw new NotImplementedException();
            }

            protected override string DataSetName()
            {
                throw new NotImplementedException();
            }
        }
    }



}
