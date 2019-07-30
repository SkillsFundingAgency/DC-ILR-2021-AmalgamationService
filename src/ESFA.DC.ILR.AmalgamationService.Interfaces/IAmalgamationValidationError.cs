using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using System.Collections.Generic;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IAmalgamationValidationError
    {
        XmlSeverityType? Severity { get; }

        AmalgamationValidationRuleType? RuleName { get; set; }

        string LearnRefNumber { get; set; }

        string Entity { get; set; }

        string Key { get; set; }

        string ConflictingAttribute { get; set; }

        string File { get; set; }

        string Value { get; set; }

        string Description { get; set; }
    }
}
