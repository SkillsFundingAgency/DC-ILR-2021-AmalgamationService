using ESFA.DC.ILR.AmalgamationService.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESFA.DC.ILR.AmalgamationService.Rules
{
    public class RuleSetExecutionService<T> : IRuleSetExecutionService<T>
        where T : class
    {
        public void Execute(IEnumerable<IRule<T>> ruleSet, T objectToValidate)
        {
            foreach (var rule in ruleSet)
            {
                rule.Validate(objectToValidate);
            }
        }
    }
}
