using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Medidata.RWS.Core.Requests.Implementations;
using Moq;
using Medidata.RWS.Core.Responses;
using RestSharp;

namespace Medidata.RWS.Tests.Core.Requests
{
    [TestClass]
    public class PostDataRequestTests
    {
        [TestMethod]
        public void PostDataRequest_properly_builds_URL()
        {
            var req = new PostDataRequest("TEST DATA");

            Assert.IsTrue(req.UrlPath().Contains("webservice.aspx?PostODMClinicalData"));

        }


        [TestMethod]
        public void PostDataRequest_data_can_be_posted_and_response_can_be_formatted_and_read()
        {


            string response_data =
            @"<Response ReferenceNumber=""82e942b0-48e8-4cf4-b299-51e2b6a89a1b""
                    InboundODMFileOID=""""
                    IsTransactionSuccessful=""1""
                    SuccessStatistics=""Rave objects touched: Subjects=0; Folders=0; Forms=0; Fields=0; LogLines=0"" NewRecords=""""
                    SubjectNumberInStudy=""999"" SubjectNumberInStudySite=""23"">
             </Response>";

            var request = new PostDataRequest("post data here!");

            var mockResponse = new Mock<IRestResponse>();
            mockResponse.Setup(x => x.Content).Returns(response_data);

            var response = request.Result(mockResponse.Object) as RWSPostResponse;

            Assert.IsInstanceOfType(response, typeof(RWSPostResponse));
            Assert.AreEqual(23, response.SubjectNumberInStudySite);
            Assert.AreEqual(999, response.SubjectNumberInStudy);


        }
    }
}
