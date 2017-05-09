using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Medidata.RWS.Core.DataBuilders;
using Moq;

namespace Medidata.RWS.Tests.Core.DataBuilders
{
    /// <summary>
    /// Summary description for FormDataBuilderTests
    /// </summary>
    [TestClass]
    public class FormDataBuilderTests
    {
        public FormDataBuilderTests()
        {

        }

      
        [TestMethod]
        public void FormDataBuilder_uses_CONTEXT_for_repeat_key_when_transaction_type_is_upsert()
        {

            var builder1 = new FormDataBuilder("FORMOID").WithTransactionType(Schema.TransactionType.Upsert);
            var builder2 = new FormDataBuilder("FORMOID", 5).WithTransactionType(Schema.TransactionType.Upsert);

            Assert.AreEqual("@CONTEXT", builder1.Build().FormRepeatKey);
            Assert.AreEqual("@CONTEXT", builder2.Build().FormRepeatKey);
        }

        [TestMethod]
        public void FormDataBuilder_uses_appropriate_repeat_key_when_supplied()
        {

            var builder1 = new FormDataBuilder("FORMOID", 2);
            Assert.AreEqual("2", builder1.Build().FormRepeatKey);

        }

        [TestMethod]
        public void FormDataBuilder_can_add_ItemGroupData_with_string_repeatkey()
        {

            var builder1 = new FormDataBuilder("STUDYOID").AddItemGroupData("TOXICITY_SOLICIT", "@CONTEXT", ig => ig.Build());

            Assert.AreEqual("@CONTEXT", builder1.Build().ItemGroupData.First().ItemGroupRepeatKey);

        }

    }
}
