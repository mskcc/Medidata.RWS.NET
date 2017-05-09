using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Medidata.RWS.Core.Requests;
using Medidata.RWS.Core.Requests.Implementations;
using Medidata.RWS.Core.Responses;
using Medidata.RWS.Core.RWSObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;

namespace Medidata.RWS.Tests.Core.Requests
{
    /// <summary>
    /// These tests make real calls to RWS over the network. We could mock these calls instead,
    /// but we've chosen not to. This helps us ensure we are communicating with RWS properly, and not
    /// making assumptions about what is being returned.
    /// </summary>
    [TestClass]
    public class CoreRequestsTests
    {

        private string _subdomain;
        private string _rwsUsername;
        private string _rwsPassword;
        private RwsConnection _connection;

        [TestInitialize]
        public void SetUp()
        {
            _subdomain = Env.Get("MEDIDATA_RAVE_DOMAIN");
            _rwsUsername = Env.Get("MEDIDATA_RAVE_USER");
            _rwsPassword = Env.Get("MEDIDATA_RAVE_PASSWORD");
            _connection = new RwsConnection(_subdomain, _rwsUsername, _rwsPassword);
        }


        [TestMethod]
        public void VersionRequest_can_propery_parse_RWS_version_number()
        {
            VersionRequest req = new VersionRequest();

            Assert.IsTrue(req.UrlPath().Contains("version"));

            IRWSResponse resp = _connection.SendRequest(req);

            Assert.IsNotNull(resp);
            Assert.IsInstanceOfType(resp, typeof(RWSTextResponse));
            Assert.AreEqual(HttpStatusCode.OK, _connection.GetLastResult().StatusCode);

        }


        [TestMethod]
        public void TwoHundredRequest_successfully_returns_RWS_info()
        {
            TwoHundredRequest req = new TwoHundredRequest();

            Assert.IsTrue(req.UrlPath().Contains("twohundred"));

            IRWSResponse resp = _connection.SendRequest(req);

            Assert.IsNotNull(resp);
            Assert.IsInstanceOfType(resp, typeof(RWSTextResponse));
            Assert.AreEqual(HttpStatusCode.OK, _connection.GetLastResult().StatusCode);

        }


        //TODO:
        //BuildVersionRequest
        //CodeNameRequest
        //DiagnosticsRequest
        //CacheFlushRequest
        //ConfigurableDatasetRequest

    }
}
