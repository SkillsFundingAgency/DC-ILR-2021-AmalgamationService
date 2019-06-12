using System;
using System.Collections.Generic;
using System.Text;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IErrorMessageParameter
    {
        string PropertyName { get; }

        string Value { get; }
    }
}
