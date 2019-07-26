using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.Model.Loose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ESFA.DC.ILR.AmalgamationService.Services.Rules
{
    public class StandardRule<T> : IRule<T>
    {
        public IRuleResult<T> Definition(IEnumerable<T> values)
        {
            if (values == null || values.Count() < 1 || values.All(x => x == null))
            {
                return new RuleResult<T>();
            }

            var distinctValues = values.Distinct().Where(x => x != null).ToList();

            if (distinctValues.Count == 1)
            {
                return new RuleResult<T>()
                {
                    Success = true,
                    AmalgamatedValue = distinctValues.First()
                };
            }
            else
            {
                return new RuleResult<T>()
                {
                    Success = false
                };
            }
        }
    }
}
