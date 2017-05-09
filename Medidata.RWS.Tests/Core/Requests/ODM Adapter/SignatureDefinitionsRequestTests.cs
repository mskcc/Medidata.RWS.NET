using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Medidata.RWS.Core.Requests.ODM_Adapter;

namespace Medidata.RWS.Tests.Core.Requests.ODM_Adapter
{
    [TestClass]
    public class SignatureDefinitionsRequestTests
    {
        [TestMethod]
        public void SignatureDefinitionsRequest_can_properly_create_a_request_with_query_string_parameters()
        {

            var sigReq = new SignatureDefinitionsRequest(ProjectName: "MediFlex(DEV)");

            Assert.AreEqual(string.Format("datasets/Signatures.odm?studyid={0}", sigReq.ProjectName.Trim()), sigReq.UrlPath());

        }
    }
}
