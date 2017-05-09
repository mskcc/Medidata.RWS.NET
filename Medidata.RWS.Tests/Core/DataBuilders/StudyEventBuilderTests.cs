using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Medidata.RWS.Core.DataBuilders;
using Moq;

namespace Medidata.RWS.Tests.Core.DataBuilders
{

    [TestClass]
    public class StudyEventBuilderTests
    {
    
        [TestMethod]
        public void StudyEventBuilder_can_add_FormData_with_string_repeatkey()
        {

            var builder1 = new StudyEventDataBuilder("STUDYOID").AddFormData("test", "@CONTEXT", fd => fd.Build());
            Assert.AreEqual("@CONTEXT", builder1.Build().FormData.First().FormRepeatKey);

        }

    }
}
