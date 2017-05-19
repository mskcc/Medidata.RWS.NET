using System;
using Medidata.RWS.Core.Requests;
using Medidata.RWS.Core.Responses;
using Medidata.RWS.Extras;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;

namespace Medidata.RWS.Tests.Extras
{
    [TestClass]
    public class AbstractParserTests
    {

        [TestMethod]
        public void AbstractParser_can_parse_invalid_xml_characters()
        {

            var mockResponse = new Mock<IRestResponse>();
            mockResponse.Setup(x => x.Content).Returns(InvalidXMLResponse);

            var response = new RWSResponse(mockResponse.Object);

            var parser = new ParserTest();

            parser.SetResponse(response);

        }



        /// <summary>
        /// A parser test class.
        /// </summary>
        /// <seealso cref="Medidata.RWS.Extras.AbstractParser" />
        public class ParserTest : AbstractParser
        {
            public override void Start()
            {
                throw new NotImplementedException();
            }
        }


        private const string InvalidXMLResponse = @"<?xml version=""1.0"" encoding=""utf-8""?>
                <ODM FileType=""Snapshot"" FileOID=""69c36236-9c95-4a23-9420-cfe94f831c8d"" CreationDateTime=""2016-11-20T21:33:00.884-00:00"" ODMVersion=""1.3"" xmlns:mdsol=""http://www.mdsol.com/ns/odm/metadata"" xmlns:xlink=""http://www.w3.org/1999/xlink"" xmlns=""http://www.cdisc.org/ns/odm/v1.3"">
                    <ClinicalData StudyOID=""Mediflex(DEV3 LabTest)"" MetaDataVersionOID=""812"">
                        <SubjectData SubjectKey=""MJK"">
                            <SiteRef LocationOID=""101"" />
                            <StudyEventData StudyEventOID=""LABS"" StudyEventRepeatKey=""LAB[1]/LABS[1]"">
                                <FormData FormOID=""HEM"" FormRepeatKey=""1"">
                                    <ItemGroupData ItemGroupOID=""HEM_LOG_LINE"">
                                        <ItemData ItemOID=""HEM.HEMDATE"" Value=""#x9"" />
                                        <ItemData ItemOID=""HEM.HEMDATE2"" Value=""#xA"" />
                                        <ItemData ItemOID=""HEM.HEMDATE3"" Value=""#xD"" />
                                        <ItemData ItemOID=""HEM.HEMDATE4"" Value=""&#x3;"" />
                                        <ItemData ItemOID=""HEM.HEMDATE5"" Value=""#x20"" />
                                        <ItemData ItemOID=""HEM.HEMDATE6"" Value=""#xD7FF"" />
                                        <ItemData ItemOID=""HEM.HEMDATE7"" Value=""#xE000"" />
                                        <ItemData ItemOID=""HEM.HEMDATE8"" Value=""#xFFFD"" />
                                        <ItemData ItemOID=""HEM.HEMDATE8"" Value=""#x10000"" />
                                        <ItemData ItemOID=""HEM.HEMDATE8"" Value=""#x10FFFF"" />
                                    </ItemGroupData>
                                </FormData>
                            </StudyEventData>
                        </SubjectData>
                    </ClinicalData>
                </ODM>";



    }






}
