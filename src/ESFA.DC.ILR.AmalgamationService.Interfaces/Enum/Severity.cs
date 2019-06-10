using System;
using System.Collections.Generic;
using System.Text;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces.Enum
{
    public enum Severity
    {
        Error,
        Warning,

        /// <summary>
        /// File is determined to be unreadable, and not able to validated.
        /// </summary>
        Fail
    }
}
