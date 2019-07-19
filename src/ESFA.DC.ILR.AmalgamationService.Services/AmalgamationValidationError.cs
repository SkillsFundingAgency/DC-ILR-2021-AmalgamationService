using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESFA.DC.ILR.AmalgamationService.Services
{
    public class AmalgamationValidationError : IAmalgamationValidationError
    {
        public Severity? Severity { get; set; }

        public AmalgamationValidationRuleType? RuleName { get; set; }

        public string LearnRefNumber { get; set; }

        public string Entity { get; set; }

        public string Key { get; set; }

        public string ConflictingAttribute { get; set; }

        public string File { get; set; }

        public string Value { get; set; }

        public string Description { get; set; }
    }
}
