using Medidata.RWS.Core.Responses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medidata.RWS.Tests.Core.Responses
{
    [TestClass]
    public class RWSErrorResponseTests
    {


        [TestMethod]
        public void RWSErrorResponse_correctly_reads_an_error_response()
        {

            string errorResponse =
                @"<Response ReferenceNumber=""0b47fe86-542f-4070-9e7d-16396a5ef08a""
                    InboundODMFileOID = ""Not Supplied""
                    IsTransactionSuccessful = ""0""
                    ReasonCode = ""RWS00092""
                    ErrorClientResponseMessage = ""CRF version not found"" >
                    </Response>";

            var response = new RWSErrorResponse(errorResponse);

            Assert.AreEqual(false, response.IsTransactionSuccessful);
            Assert.AreEqual("CRF version not found", response.ErrorDescription);
            Assert.AreEqual("Not Supplied", response.InboundODMFileOID);
            Assert.AreEqual("RWS00092", response.ReasonCode);
            Assert.AreEqual("0b47fe86-542f-4070-9e7d-16396a5ef08a", response.ReferenceNumber);

        }




    }



}
