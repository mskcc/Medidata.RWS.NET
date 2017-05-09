using Medidata.RWS.Schema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Medidata.RWS.Core
{
    /// <summary>
    /// Helpers to assist with Rave Web Services operations.
    /// </summary>
    public static class RWSHelpers
    {

        /// <summary>
        /// Serialize objects from XML.
        /// </summary>
        public static class Serializers
        {
            /// <summary>
            /// Deserialize a strongly typed object from an XML string.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="xmlString"></param>
            /// <returns></returns>
            public static T XmlDeserializeFromString<T>(string xmlString)
            {
                return (T)XmlDeserializeFromString(xmlString, typeof(T));
            }

            /// <summary>
            /// Deserialize a generic object from an XML string.
            /// </summary>
            /// <param name="objectData"></param>
            /// <param name="type"></param>
            /// <returns></returns>
            private static object XmlDeserializeFromString(string objectData, Type type)
            {

                //remove the first ByteOrderMark (BOM) if it exists
                //see http://stackoverflow.com/questions/1317700/strip-byte-order-mark-from-string-in-c-sharp
                objectData = objectData.Trim(new char[] { '\uFEFF', '\u200B' });

                var serializer = new XmlSerializer(type);
                object result;

                using (TextReader reader = new StringReader(objectData))
                {
                    result = serializer.Deserialize(reader);
                }

                return result;
            }

        }

        /// <summary>
        /// The UTF-8 Encoding.
        /// </summary>
        /// <seealso cref="System.IO.StringWriter" />
        public class Utf8StringWriter : StringWriter
        {
            /// <summary>
            /// Gets the <see cref="T:System.Text.Encoding" /> in which the output is written.
            /// </summary>
            public override Encoding Encoding
            {
                get { return new UTF8Encoding(false); } 
            }
        }


        /// <summary>
        /// Basic RWS Helpers
        /// </summary>
        public static class Helpers
        {

            /// <summary>
            /// Return the environment name based on a study and protocol name.
            /// </summary>
            /// <param name="StudyName"></param>
            /// <param name="ProtocolName"></param>
            /// <returns></returns>
            public static string GetEnvironmentFromStudyNameAndProtocol(string StudyName, string ProtocolName)
            {

                var raw_environment = StudyName.Substring(ProtocolName.Length).Trim();
                if (raw_environment.Contains("("))
                {
                    var L_BracePos = raw_environment.IndexOf("(");
                    var R_BracePos = raw_environment.IndexOf(")");

                    return raw_environment.Substring(L_BracePos + 1, R_BracePos - 1);
                }
                else
                {
                    return raw_environment;
                }
            }



            /// <summary>
            /// Gets the environment name from a study oid.
            /// </summary>
            /// <param name="StudyOID">The study oid.</param>
            /// <returns></returns>
            public static string GetEnvironmentNameFromStudyOID(string StudyOID)
            {
                if (StudyOID.EndsWith(")") && StudyOID.Contains("("))
                {
                    var L_BracePos = StudyOID.IndexOf("(");
                    var R_BracePos = StudyOID.IndexOf(")");

                    return StudyOID.Substring(L_BracePos + 1, (R_BracePos -1) - L_BracePos).Trim();
                }
                else
                {
                    return string.Empty;
                }
            }


            /// <summary>
            /// Gets the project name from study oid.
            /// </summary>
            /// <param name="StudyOID">The study oid.</param>
            /// <returns></returns>
            public static string GetProjectNameFromStudyOID(string StudyOID)
            {
                if (StudyOID.EndsWith(")") && StudyOID.Contains("("))
                {
                    var L_BracePos = StudyOID.IndexOf("(");
                    return StudyOID.Substring(0, L_BracePos).Trim();
                }
                else
                {
                    return StudyOID.Trim();
                }
            }

        }


    }
}
