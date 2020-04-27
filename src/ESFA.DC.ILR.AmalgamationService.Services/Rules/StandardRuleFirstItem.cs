using ESFA.DC.ILR.AmalgamationService.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ESFA.DC.ILR.AmalgamationService.Services.Rules
{
    public class StandardRuleFirstItem<T> : IRule<T>
    {
        public IRuleResult<T> Definition(IEnumerable<T> values)
        {
            var standardRule = new StandardRule<T>();
            var result = standardRule.Definition(values);
            if (!result.Success)
            {
                return new RuleResult<T> { Success = false, AmalgamatedValue = values.FirstOrDefault(x => x != null) };
            }

            return result;
        }
    }
}
