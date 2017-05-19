using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medidata.RWS.Extras.AuditEvent
{
    /// <summary>
    /// The MeasurementUnitRef element
    /// </summary>
    /// <seealso cref="Medidata.RWS.Extras.AuditEvent.ContextBase" />
    public class MeasurementUnitRef : ContextBase
    {
        /// <summary>
        /// Gets the measurement unit oid.
        /// </summary>
        /// <value>
        /// The measurement unit oid.
        /// </value>
        public string MeasurementUnitOID { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MeasurementUnitRef"/> class.
        /// </summary>
        public MeasurementUnitRef()
        {
            MeasurementUnitOID = "";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MeasurementUnitRef"/> class.
        /// </summary>
        public MeasurementUnitRef(string measurementUnitOID)
        {
            MeasurementUnitOID = measurementUnitOID;
        }
    }
}
