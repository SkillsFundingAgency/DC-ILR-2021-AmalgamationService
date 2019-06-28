using ESFA.DC.ILR.AmalgamationService.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESFA.DC.ILR.AmalgamationService.Services.Rules
{
    public class RuleResult<T> : IRuleResult<T>
    {
        public bool Success { get; set; }

        public T Result { get; set; } = default(T);
    }
}
