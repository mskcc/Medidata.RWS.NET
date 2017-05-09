namespace Medidata.RWS
{
    using Medidata.RWS.Schema;

    /// <summary>
    /// A list of "constants" 
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// The odm namespace.
        /// </summary>
        public const string ODM_NS = "http://www.cdisc.org/ns/odm/v1.3";

        /// <summary>
        /// The mdsol namespace.
        /// </summary>
        public const string MDSOL_NS = "http://www.mdsol.com/ns/odm/metadata";

        /// <summary>
        /// The default transaction type
        /// </summary>
        public const TransactionType DEFAULT_TRANSACTION_TYPE = TransactionType.Upsert;

        /************************************************************
         Represents HTTP Response Header values pertaining to Links.
         Based on https://github.com/eclipse/egit-github/blob/master/org.eclipse.egit.github.core/src/org/eclipse/egit/github/core/client/IGitHubConstants.java
        /************************************************************/

        /// <summary>
        /// Gets the header link attribute.
        /// </summary>
        /// <value>
        /// The header link attribute.
        /// </value>
        public static string HEADER_LINK
        {
            get { return "Link"; }
        }

        /// <summary>
        /// Gets the header next attribute.
        /// </summary>
        /// <value>
        /// The header next attribute.
        /// </value>
        public static string HEADER_NEXT
        {
            get { return "X-Next"; }
        }

        /// <summary>
        /// Gets the header last attribute.
        /// </summary>
        /// <value>
        /// The header last attribute.
        /// </value>
        public static string HEADER_LAST
        {
            get { return "X-Last"; }
        }

        /// <summary>
        /// Gets the meta relative attribute.
        /// </summary>
        /// <value>
        /// The meta relative attribute.
        /// </value>
        public static string META_REL
        {
            get { return "rel"; }
        }

        /// <summary>
        /// Gets the meta last attribute.
        /// </summary>
        /// <value>
        /// The meta last attribute.
        /// </value>
        public static string META_LAST
        {
            get { return "last"; }
        }

        /// <summary>
        /// Gets the meta next attribute.
        /// </summary>
        /// <value>
        /// The meta next attribute.
        /// </value>
        public static string META_NEXT
        {
            get { return "next"; }
        }

        /// <summary>
        /// Gets the meta first attribute.
        /// </summary>
        /// <value>
        /// The meta first attribute.
        /// </value>
        public static string META_FIRST
        {
            get { return "first"; }
        }

        /// <summary>
        /// Gets the meta previous attribute.
        /// </summary>
        /// <value>
        /// The meta previous attribute.
        /// </value>
        public static string META_PREV
        {
            get { return "prev"; }
        }

        /************************************************************/


    }
}
