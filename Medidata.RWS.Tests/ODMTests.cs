using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Medidata.RWS.Core.DataBuilders;
using Medidata.RWS;
using System.Linq;
using Medidata.RWS.Schema;

namespace Medidata.RWS.Tests
{
    [TestClass]
    public class ODMTests
    {
        [TestMethod]
        public void default_odm_version_is_1_dot_3()
        {

            var builder = new ODMBuilder();

            Assert.IsTrue(builder.Build().ODMVersion == ODMVersion.Item13);

        }

        [TestMethod]
        public void FindClinicalDataWithStudyOID_method_returns_clinical_data_node_if_exists()
        {

            var odm = new ODMBuilder().WithClinicalData("TestStudyOID", cdb => cdb.Build()).Build();

            Assert.IsNotNull(odm.FindClinicalDataWithStudyOID("TestStudyOID"));

        }

        [TestMethod]
        public void clinical_data_is_not_added_twice_for_same_study_OID()
        {

            var odm = new ODMBuilder()
                .WithClinicalData("TestStudyOID", cdb => cdb.Build())
                .WithClinicalData("TestStudyOID", cdb => cdb.Build()).Build();

            Assert.IsTrue(odm.ClinicalData.Count == 1);

        }

        [TestMethod]
        public void item_group_data_builder_can_remove_all_empty_itemdata_nodes()
        {

            var RegistrationData = new ODMBuilder()
                    .WithClinicalData("TEST STUDY OID", cd =>
                        cd.WithSubjectData("10000", "101", sd =>
                            sd.WithTransactionType(TransactionType.Insert)
                               .AddStudyEventData("SCREEN", 1, sed =>
                                    sed.WithTransactionType(TransactionType.Update)
                                       //DM (Demographics form)
                                       .AddFormData("DM", 1, fd =>
                                          fd.WithTransactionType(TransactionType.Update).AddItemGroupData("DM", igd =>
                                              igd.WithTransactionType(TransactionType.Update)
                                                    // DM form fields
                                                    .AddItemData("BRTHDAT", DateTime.Now.ToString("dd MMM yyyy"), id =>
                                                        id.WithTransactionType(TransactionType.Update))
                                                    .AddItemData("SUBJECTINIT", "MK", id =>
                                                        id.WithTransactionType(TransactionType.Update))
                                                    .AddItemData("SEX", "2", id =>
                                                        id.WithTransactionType(TransactionType.Update))
                                                    .AddItemData("ETHNIC", "2", id =>
                                                        id.WithTransactionType(TransactionType.Update))
                                                    .AddItemData("RACE", "", id =>
                                                        id.WithTransactionType(TransactionType.Update)).RemoveEmptyNodes()))

            )));


            Assert.IsTrue(RegistrationData.Build().ClinicalData.First().SubjectData.First().StudyEventData.First().FormData.First().ItemGroupData.First().Items.Count == 4);

        }




    }


}
