using System;
using System.Collections.Generic;
using System.Text;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IRuleResult<T>
    {
        bool Success { get; }

        T AmalgamatedValue { get; }

        IEnumerable<IAmalgamationValidationError> AmalgamationValidationErrors { get; }
    }
}
