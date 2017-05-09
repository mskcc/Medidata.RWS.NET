using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Medidata.RWS.Core.DataBuilders;
using Moq;

namespace Medidata.RWS.Tests.Core.DataBuilders
{

    [TestClass]
    public class ItemGroupDataBuilderTests
    {
      

        [TestMethod]
        public void ItemGroupDataBuilderTests_uses_appropriate_repeat_key_when_supplied()
        {

            var builder1 = new ItemGroupDataBuilder("FORMOID", "@CONTEXT");
            Assert.AreEqual("@CONTEXT", builder1.Build().ItemGroupRepeatKey);

        }

    }
}
