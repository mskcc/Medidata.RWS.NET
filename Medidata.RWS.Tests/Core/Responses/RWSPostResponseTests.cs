using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Medidata.RWS.Core.Responses;
using System.Collections.Generic;
using Moq;
using RestSharp;

namespace Medidata.RWS.Tests.Core.Responses
{
    [TestClass]
    public class RWSPostResponseTests
    {
        [TestMethod]
        public void RWSPostResponse_correctly_reads_a_response()
        {

            string response =
            @"<Response ReferenceNumber=""82e942b0-48e8-4cf4-b299-51e2b6a89a1b""
                    InboundODMFileOID=""""
                    IsTransactionSuccessful=""1""
                    SuccessStatistics=""Rave objects touched: Subjects=0; Folders=0; Forms=0; Fields=0; LogLines=0"" NewRecords=""""
                    SubjectNumberInStudy=""999"" SubjectNumberInStudySite=""23"">
             </Response>";

            var mockResponse = new Mock<IRestResponse>();
            mockResponse.Setup(x => x.Content).Returns(response);

            var resp = new RWSPostResponse(mockResponse.Object);

            Assert.IsTrue(resp.IsTransactionSuccessful);
            Assert.AreEqual(999, resp.SubjectNumberInStudy);
            Assert.AreEqual(23, resp.SubjectNumberInStudySite);

        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void RWSPostResponse_throws_exception_when_statistics_contains_unknown_fields()
        {

            string response =
            @"<Response ReferenceNumber=""82e942b0-48e8-4cf4-b299-51e2b6a89a1b""
                    InboundODMFileOID=""""
                    IsTransactionSuccessful=""1""
                    SuccessStatistics=""Rave objects touched: Subjects=0; Folders=0; Unknown=10; Forms=0; Fields=0; LogLines=0"" NewRecords=""""
                    SubjectNumberInStudy=""999"" SubjectNumberInStudySite=""23"">
             </Response>";

            var mockResponse = new Mock<IRestResponse>();
            mockResponse.Setup(x => x.Content).Returns(response);
            var resp = new RWSPostResponse(mockResponse.Object);
        }



    }
}
