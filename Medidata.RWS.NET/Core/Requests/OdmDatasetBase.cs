using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medidata.RWS.Core.Requests
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class OdmDatasetBase : QueryOptionGetRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OdmDatasetBase" /> class.
        /// </summary>
        /// <param name="dataset_type">Type of the dataset.</param>
        /// <param name="versionitem">The versionitem.</param>
        /// <param name="rawsuffix">The rawsuffix.</param>
        /// <param name="codelistsuffix">The codelistsuffix.</param>
        /// <param name="decodesuffix">The decodesuffix.</param>
        /// <param name="stdsuffix">The stdsuffix.</param>
        /// <param name="start">The start.</param>
        protected OdmDatasetBase(string dataset_type, string versionitem, string rawsuffix, string codelistsuffix, string decodesuffix, string stdsuffix, string start)
        {
            this.dataset_type = dataset_type.ToLower();
            this.versionitem = versionitem;
            this.rawsuffix = rawsuffix;
            this.codelistsuffix = codelistsuffix;
            this.decodesuffix = decodesuffix;
            this.stdsuffix = stdsuffix;
            this.start = start;
        }

        /// <summary>
        /// Gets or sets the type of the dataset.
        /// </summary>
        /// <value>
        /// The type of the dataset.
        /// </value>
        public string dataset_type { get; set; }

        /// <summary>
        /// Gets the versionitem.
        /// </summary>
        /// <value>
        /// The versionitem.
        /// </value>
        public string versionitem { get; private set; }
        /// <summary>
        /// Gets the rawsuffix.
        /// </summary>
        /// <value>
        /// The rawsuffix.
        /// </value>
        public string rawsuffix { get; private set; }
        /// <summary>
        /// Gets the codelistsuffix.
        /// </summary>
        /// <value>
        /// The codelistsuffix.
        /// </value>
        public string codelistsuffix { get; private set; }
        /// <summary>
        /// Gets the decodesuffix.
        /// </summary>
        /// <value>
        /// The decodesuffix.
        /// </value>
        public string decodesuffix { get; private set; }
        /// <summary>
        /// Gets the stdsuffix.
        /// </summary>
        /// <value>
        /// The stdsuffix.
        /// </value>
        public string stdsuffix { get; private set; }
        /// <summary>
        /// Gets the start.
        /// </summary>
        /// <value>
        /// The start.
        /// </value>
        public string start { get; private set; }

        /// <inheritdoc />
        public override List<string> KnownQueryOptions => new List<string>
        {
            "versionitem", "rawsuffix", "codelistsuffix", "decodesuffix", "stdsuffix", "start" 
        };

        /// <summary>
        /// Create a new key value list of query string parameters.
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, string> QueryString()
        {
            var parameters = base.QueryString();

            if (parameters.ContainsKey("start"))
            {
                //TODO: format date to ISO
                //https://github.com/mdsol/rwslib/blob/96715e4ff2ae97f5fa96bb856e4488a96fabce99/rwslib/rws_requests/__init__.py
            }

            return parameters;

        }


        /// <summary>
        /// Verifies this instance.
        /// </summary>
        public void Verify()
        {
            if (dataset_type.ToLower() == "regular" || dataset_type.ToLower() == "raw") return;
            
            throw new NotSupportedException(
                $"dataset_type must be 'regular' or 'raw'. Supplied value is '{dataset_type}'.");
            
        }
    }
}
