using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using System.Collections.Generic;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IAmalgamationValidationError
    {
        string RuleName { get; }

        Severity? Severity { get; }

        IEnumerable<IErrorMessageParameter> ErrorMessageParameters { get; }
    }
}
