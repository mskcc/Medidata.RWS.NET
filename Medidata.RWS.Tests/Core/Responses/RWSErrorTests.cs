using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Medidata.RWS.Core.Responses;

namespace Medidata.RWS.Tests.Core.Responses
{
    [TestClass]
    public class RWSErrorTests
    {
        [TestMethod]
        public void RWSError_correctly_reads_an_error_response()
        {

            string errorResponse =
                @"<?xml version=""1.0"" encoding=""utf-8""?>
                     <ODM xmlns:mdsol=""http://www.mdsol.com/ns/odm/metadata""
                     FileType=""Snapshot""
                     CreationDateTime=""2013-04-08T10:28:49.578-00:00""
                     FileOID=""4d13722a-ceb6-4419-a917-b6ad5d0bc30e""
                     ODMVersion=""1.3""
                     mdsol:ErrorDescription=""Incorrect login and password combination. [RWS00008]""
                     xmlns=""http://www.cdisc.org/ns/odm/v1.3"" /> ";

            var error = new RwsError(errorResponse);

            Assert.AreEqual("4d13722a-ceb6-4419-a917-b6ad5d0bc30e", error.FileOID);
            Assert.AreEqual("Incorrect login and password combination. [RWS00008]", error.ErrorDescription);
            Assert.AreEqual(DateTime.Parse("2013-04-08T10:28:49.578-00:00"), error.CreationDateTime);
            Assert.AreEqual(Schema.ODMVersion.Item13, error.ODMVersion);
            Assert.AreEqual(Schema.FileType.Snapshot, error.FileType);

        }
    }
}
