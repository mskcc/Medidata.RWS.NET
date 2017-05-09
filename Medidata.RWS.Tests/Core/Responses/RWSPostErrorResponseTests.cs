using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Medidata.RWS.Core.Responses;
using Moq;
using RestSharp;

namespace Medidata.RWS.Tests.Core.Responses
{
    [TestClass]
    public class RWSPostErrorResponseTests
    {
        [TestMethod]
        public void RWSPostErrorResponse_correctly_reads_a_POST_response()
        {

            string errorResponse =
                @"<Response ReferenceNumber=""0b47fe86-542f-4070-9e7d-16396a5ef08a""
                    InboundODMFileOID=""1""
                    IsTransactionSuccessful=""0""
                    ReasonCode =""RWS00024""
                    ErrorOriginLocation=""/ODM/ClinicalData[1]/SubjectData[1]""
                    SuccessStatistics=""Rave objects touched: Subjects=0; Folders=0; Forms=0; Fields=0; LogLines=0""
                    ErrorClientResponseMessage =""Subject already exists."">
                    </Response>";

            var mockResponse = new Mock<IRestResponse>();
            mockResponse.Setup(x => x.Content).Returns(errorResponse);

            var response = new RWSPostErrorResponse(mockResponse.Object);

            Assert.AreEqual("0b47fe86-542f-4070-9e7d-16396a5ef08a", response.ReferenceNumber);
            Assert.AreEqual("Subject already exists.", response.ErrorClientResponseMessage);
            Assert.AreEqual("/ODM/ClinicalData[1]/SubjectData[1]", response.ErrorOriginLocation);

        }
    }
}
