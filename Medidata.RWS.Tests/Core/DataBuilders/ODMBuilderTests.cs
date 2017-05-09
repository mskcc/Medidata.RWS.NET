using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Medidata.RWS.Core.DataBuilders;
using Medidata.RWS.Schema;

namespace Medidata.RWS.Tests.Core.DataBuilders
{
    [TestClass]
    public class ODMBuilderTests
    {
        [TestMethod]
        public void ODMBuilder_AsXMLString_creates_valid_ClinicalData_node()
        {

            var registrationXml = new ODMBuilder().WithClinicalData("ClinicalTest", cd =>
                cd.WithSubjectData("1", "101", sd =>
                    sd.WithTransactionType(TransactionType.Insert))).AsXMLString();

            const string xml = @"<ClinicalData StudyOID=""ClinicalTest""";

            Assert.IsTrue(registrationXml.Contains(xml));
        }


        [TestMethod]
        public void ODMBuilder_adds_xmlns_mdsol_attribute_to_ODM_node_in_XML()
        {

            var registrationXml = new ODMBuilder().WithClinicalData("ClinicalTest", cd =>
                cd.WithSubjectData("1", "101", sd =>
                    sd.WithTransactionType(TransactionType.Insert))).AsXMLString();

            const string xml = @"xmlns:mdsol=""http://www.mdsol.com/ns/odm/metadata""";

            Assert.IsTrue(registrationXml.Contains(xml));
        }


        [TestMethod]
        public void ODMBuilder_does_not_serialize_StudyEventRepeatKey_if_not_supplied()
        {
            var b1 = new ODMBuilder().WithClinicalData("TESTOID", cd =>
                cd.WithSubjectData("SUBJECT", "1", sdb => sdb.AddStudyEventData("TEST", seb => seb.Build())));

            var b2 = new ODMBuilder().WithClinicalData("TESTOID", cd =>
                cd.WithSubjectData("SUBJECT", "1", sdb => sdb.AddStudyEventData("TEST", 1, seb => seb.Build())));

            var nokey =
                b1.AsXDocument()
                    .Descendants(XNamespace.Get(Constants.ODM_NS) + "StudyEventData")
                    .First()
                    .Attribute("StudyEventRepeatKey");

            Assert.IsNull(nokey);


            var haskey =
                b2.AsXDocument()
                    .Descendants(XNamespace.Get(Constants.ODM_NS) + "StudyEventData")
                    .First()
                    .Attribute("StudyEventRepeatKey");

            Assert.IsNotNull(haskey);

        }


        [TestMethod]
        public void ODMBuilder_does_not_serialize_FormDataRepeatKey_if_not_supplied()
        {
            var b1 = new ODMBuilder().WithClinicalData("TESTOID", cd =>
                cd.WithSubjectData("SUBJECT", "1", sdb => sdb.AddStudyEventData("TEST", seb =>
                    seb.AddFormData("FORMOID", fd => fd.Build()))));

            var b2 = new ODMBuilder().WithClinicalData("TESTOID", cd =>
                cd.WithSubjectData("SUBJECT", "1", sdb => sdb.AddStudyEventData("TEST", seb =>
                    seb.AddFormData("FORMOID", 1, fd => fd.Build()))));

            var nokey =
                b1.AsXDocument()
                    .Descendants(XNamespace.Get(Constants.ODM_NS) + "FormData")
                    .First()
                    .Attribute("FormRepeatKey");

            Assert.IsNull(nokey);


            var haskey =
                b2.AsXDocument()
                    .Descendants(XNamespace.Get(Constants.ODM_NS) + "FormData")
                    .First()
                    .Attribute("FormRepeatKey");

            Assert.IsNotNull(haskey);

        }


        [TestMethod]
        public void ODMBuilder_does_not_serialize_ItemGroupRepeatKey_if_not_supplied()
        {
            var b1 = new ODMBuilder().WithClinicalData("TESTOID", cd =>
                cd.WithSubjectData("SUBJECT", "1", sdb => sdb.AddStudyEventData("TEST", seb =>
                    seb.AddFormData("FORMOID", fd => fd.AddItemGroupData("ITEMGROUP", igd => igd.Build())))));

            var b2 = new ODMBuilder().WithClinicalData("TESTOID", cd =>
                cd.WithSubjectData("SUBJECT", "1", sdb => sdb.AddStudyEventData("TEST", seb =>
                    seb.AddFormData("FORMOID", fd => fd.AddItemGroupData("ITEMGROUP", "1", igd => igd.Build())))));

            var nokey =
                b1.AsXDocument()
                    .Descendants(XNamespace.Get(Constants.ODM_NS) + "ItemGroupData")
                    .First()
                    .Attribute("ItemGroupRepeatKey");

            Assert.IsNull(nokey);


            var haskey =
                b2.AsXDocument()
                    .Descendants(XNamespace.Get(Constants.ODM_NS) + "ItemGroupData")
                    .First()
                    .Attribute("ItemGroupRepeatKey");

            Assert.IsNotNull(haskey);

        }
    }
}
