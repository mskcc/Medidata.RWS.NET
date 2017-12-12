using System;
using System.Net;
using Medidata.RWS.Core.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Medidata.RWS.Core.Requests;
using Medidata.RWS.Core.Responses;

namespace Medidata.RWS.Tests.Integration
{
    [TestClass]
    public class SecurityProtocolTest_MedidataRAVE_06JAN18
    {
        readonly string MEDIDATA_RAVE_TLS_TEST_SITE = "secops-rave-test";
        protected RwsConnection rws;
        protected SecurityProtocolType defaultSecurityProtocolType;
        [TestInitialize]
        public void InitRwsConn()
        {
            defaultSecurityProtocolType = ServicePointManager.SecurityProtocol;
            rws = new RwsConnection(MEDIDATA_RAVE_TLS_TEST_SITE);
        }


        [TestMethod]
        [ExpectedException(typeof(RWSException))]
        public void SSL_BasicRWSRequest_ShouldExceptionWithStatusCode0()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            var response = rws.SendRequest(new VersionRequest()) as RWSTextResponse;
            Assert.Fail();
        }
        [TestMethod]
        [ExpectedException(typeof(RWSException))]
        public void TLS10_BasicRWSRequest_ShouldExceptionWithStatusCode0()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            var response = rws.SendRequest(new VersionRequest()) as RWSTextResponse;
            Assert.Fail();
        }
        [TestMethod]
        [ExpectedException(typeof(RWSException))]
        public void TLS11_BasicRWSRequest_ShouldExceptionWithStatusCode0()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11;
            var response = rws.SendRequest(new VersionRequest()) as RWSTextResponse;
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(RWSException))]
        public void DotNetDefault_BasicRWSRequest_ShouldExceptionWithStatusCode0()
        {
            ServicePointManager.SecurityProtocol = defaultSecurityProtocolType;
            var response = rws.SendRequest(new VersionRequest()) as RWSTextResponse;
            Assert.Fail();
        }
        [TestMethod]
        public void TLS12_BasicRWSRequest_ShouldReturnTextResponse()
        {
            try
            {
                var response = rws.SendRequest(new VersionRequest()) as RWSTextResponse;
                var expected = "1.16.0";
                var actual = response.ResponseText;
                Assert.AreEqual(expected, actual);
            }
            catch (RWSException rwse)
            {
                Assert.Fail(rwse.Message);
            }
            
            
        }

     
    }
}
