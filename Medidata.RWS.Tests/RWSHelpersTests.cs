using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Medidata.RWS.Core;

namespace Medidata.RWS.Tests
{
    [TestClass]
    public class RWSHelpersTests
    {
        [TestMethod]
        public void RequestHelpers_can_parse_study_oid_into_project_and_environment_names()
        {

            string studyOID = "Teststudy(DEV)";

            var p = RWSHelpers.Helpers.GetProjectNameFromStudyOID(studyOID);
            var e = RWSHelpers.Helpers.GetEnvironmentNameFromStudyOID(studyOID);

            Assert.AreEqual("Teststudy", p);
            Assert.AreEqual("DEV", e);

            studyOID = "Teststudy (DEV)";

            p = RWSHelpers.Helpers.GetProjectNameFromStudyOID(studyOID);
            e = RWSHelpers.Helpers.GetEnvironmentNameFromStudyOID(studyOID);

            Assert.AreEqual("Teststudy", p);
            Assert.AreEqual("DEV", e);

            studyOID = "Teststudy";

            p = RWSHelpers.Helpers.GetProjectNameFromStudyOID(studyOID);
            e = RWSHelpers.Helpers.GetEnvironmentNameFromStudyOID(studyOID);

            Assert.AreEqual("Teststudy", p);
            Assert.AreEqual("", e);

            studyOID = "Teststudy(PROD)";

            p = RWSHelpers.Helpers.GetProjectNameFromStudyOID(studyOID);
            e = RWSHelpers.Helpers.GetEnvironmentNameFromStudyOID(studyOID);

            Assert.AreEqual("Teststudy", p);
            Assert.AreEqual("PROD", e);


        }
    }
}
