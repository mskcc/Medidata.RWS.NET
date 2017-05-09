using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medidata.RWS.Core.DataBuilders
{
    /// <summary>
    /// Validatable interface.
    /// </summary>
    /// <tocexclude />
    public interface Validatable
    {
        /// <summary>
        /// Validates this instance.
        /// </summary>
        void Validate();

    }
}
