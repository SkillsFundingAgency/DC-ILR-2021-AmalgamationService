using ESFA.DC.ILR.AmalgamationService.Interfaces;
using System.Collections.Generic;

namespace ESFA.DC.ILR.AmalgamationService.Services.Rules
{
    public class RuleResult<T> : IRuleResult<T>
    {
        public bool Success { get; set; }

        public T AmalgamatedValue { get; set; }

        public IEnumerable<IAmalgamationValidationError> AmalgamationValidationErrors { get; set; }
    }
}
