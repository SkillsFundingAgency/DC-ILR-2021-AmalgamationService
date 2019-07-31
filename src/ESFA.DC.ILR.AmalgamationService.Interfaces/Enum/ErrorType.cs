using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces.Enum
{
    public enum ErrorType
    {
        [Description("Field value conflict")]
        FieldValueConflict,

        [Description("Max occurrence exceeded for this field type")]
        MaxOccurrenceExceeded,
    }
}
