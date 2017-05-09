using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Medidata.RWS.Core.Requests;

namespace Medidata.RWS.Tests.Core
{
    [TestClass]
    public class RequestHelpersTests
    {
        [TestMethod]
        public void RequestHelpers_MakeUrl_method_encodes_a_study_with_a_slash_in_the_name()
        {

            var url = RequestHelpers.MakeUrl("/", "Mediflex_/11/11", "TEST_RESOURCE");

            Assert.AreEqual("Mediflex_%2f11%2f11/TEST_RESOURCE", url);

            url = RequestHelpers.MakeUrl("/", "webservice.aspx?PostODMClinicalData");

            Assert.AreEqual("webservice.aspx?PostODMClinicalData", url);

        }

    }
}
